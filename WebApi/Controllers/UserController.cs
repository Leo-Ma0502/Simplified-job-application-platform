using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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

                newUser.Password = null;

                return CreatedAtAction(nameof(Get), new { id = newUser.UId }, newUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

    }
}