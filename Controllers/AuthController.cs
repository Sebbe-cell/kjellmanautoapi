using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kjellmanautoapi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<UserLoginDto>>> Register(UserLoginDto newUser)
        {
            var response = await _authService.Register(newUser);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPost("login"), AllowAnonymous]
        public async Task<ActionResult<LoginResponseDto>> Login(UserLoginDto userLogin)
        {
            if (userLogin == null)
            {
                return BadRequest("Invalid input"); // Return a 400 Bad Request with an error message for invalid input
            }

            var serviceResponse = await _authService.Login(userLogin.UserName, userLogin.PasswordHash);

            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse); // Return a 400 Bad Request with error message
            }

            if (serviceResponse.Data == null)
            {
                return BadRequest("Invalid user data"); // Return a 400 Bad Request with an error message for invalid user data
            }

            var token = _authService.CreateToken(serviceResponse.Data);

            var loginResponse = new LoginResponseDto
            {
                UserData = serviceResponse.Data,
                Token = token
            };

            return Ok(loginResponse); // Return a 200 OK with user data and token
        }
    }
}