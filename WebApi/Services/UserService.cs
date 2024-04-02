using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<User> RegisterAsync(User user)
        {
            return await _userRepository.RegisterAsync(user);
        }

        public async Task<User> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepository.FindByEmailAsync(email);
            if (user == null)
            {
                return null;
            }

            var hashedPassword = PasswordHasher.HashedPassword(password, user.Salt);
            if (hashedPassword != user.Password)
            {
                return null;
            }

            return user;
        }


        public async Task<bool> Exists(string email)
        {
            return await _userRepository.FindByEmailAsync(email) != null;
        }

    }
}