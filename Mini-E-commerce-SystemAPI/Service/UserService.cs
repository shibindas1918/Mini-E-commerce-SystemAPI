using Mini_E_commerce_SystemAPI.Interfaces;
using Mini_E_commerce_SystemAPI.Models;

namespace Mini_E_commerce_SystemAPI.Service
{
    public class UserService : IUserService
    {
        public Task<User> AuthenticateUserAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<string> GenerateJwtTokenAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> RegisterUserAsync(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
