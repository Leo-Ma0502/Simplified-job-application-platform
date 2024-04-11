using WebApi.Models;

namespace WebApi.Repositories
{
    public interface IUserRepository
    {
        Task<User> FindByEmailAsync(string email);
        Task<User> GetUserByIdAsync(int id);
        Task<User> RegisterAsync(User user);
    }
}