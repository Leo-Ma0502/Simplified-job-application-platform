using WebApi.Models;

namespace WebApi.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int id);
    }
}