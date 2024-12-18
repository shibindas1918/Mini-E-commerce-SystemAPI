using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Mini_E_commerce_SystemAPI.Interfaces;
using Mini_E_commerce_SystemAPI.Models;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dapper;
using Microsoft.AspNet.Identity;

namespace Mini_E_commerce_SystemAPI.Service
{
    public class UserService : IUserService
    {
        private readonly IDbConnection _dbConnection;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IDbConnection dbConnection, IConfiguration configuration, IPasswordHasher passwordHasher)
        {
            _dbConnection = dbConnection;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
        }

        // Register a new user
        public async Task<User> RegisterUserAsync(string username, string password)
        {
            var passwordHash = _passwordHasher.HashPassword(password);
            var user = new User
            {
                Username = username,
                PasswordHash = passwordHash,
                Role = "Customer" // Default role for now
            };

            // Insert into Users table
            var query = "INSERT INTO Users (Username, PasswordHash, Role) OUTPUT INSERTED.UserId VALUES (@Username, @PasswordHash, @Role)";
            user.UserId = await _dbConnection.ExecuteScalarAsync<int>(query, user);

            return user;
        }

        // Authenticate user
        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            var query = "SELECT * FROM Users WHERE Username = @Username";
            var user = await _dbConnection.QueryFirstOrDefaultAsync<User>(query, new { Username = username });
            if (user == null || _passwordHasher.VerifyHashedPassword(user.PasswordHash, password) != Microsoft.AspNet.Identity.PasswordVerificationResult.Success)
            {
                return null; // Authentication failed
            }
            return user; // Authentication successful
        }

        // Generate JWT Token
        public async Task<string> GenerateJwtTokenAsync(User user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}

