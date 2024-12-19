using Mini_E_commerce_SystemAPI.Models;

namespace Mini_E_commerce_SystemAPI.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterUserAsync(string username, string password);
        Task<User> AuthenticateUserAsync(string username, string password);
        Task<string> GenerateJwtTokenAsync(User user);
        Task<IEnumerable<User>> GetUsers();
    }

}
