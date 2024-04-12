using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Services;
using WebApi.DTO;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly ITokenService _tokenService;

        public UserController(IUserService userService, ITokenService tokenService = null)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _userService.Exists(user.Email))
            {
                return BadRequest("User already exists.");
            }

            try
            {
                var newUser = await _userService.RegisterAsync(user);
                if (newUser == null)
                {
                    return BadRequest("Registration failed due to an unexpected error.");
                }

                LoginDTO loginDto = new LoginDTO()
                {
                    Email = newUser.Email,
                    Password = newUser.Password
                };
                await Login(loginDto);
                newUser.Password = null;
                return CreatedAtAction(nameof(Get), new { id = newUser.UId }, newUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var user = await _userService.AuthenticateAsync(loginDto.Email, loginDto.Password);

            if (user == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            var token = await _tokenService.GenerateToken(user);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(1)
            };

            Response.Cookies.Append("AuthToken", token, cookieOptions);

            return Ok(new { Message = "Login successful", Name = user.FirstName });
        }

    }
}