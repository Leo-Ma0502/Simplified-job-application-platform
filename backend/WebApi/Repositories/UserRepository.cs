using WebApi.Models;
using WebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> RegisterAsync(User user)
        {
            byte[] salt;
            user.Password = PasswordHasher.HashPassword(user.Password, out salt);
            user.Salt = salt;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

    }
}


