using WebApi.Models;

namespace WebApi.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User> RegisterAsync(User user);
        Task<User> AuthenticateAsync(string email, string password);
        Task<bool> Exists(string email);

    }
}