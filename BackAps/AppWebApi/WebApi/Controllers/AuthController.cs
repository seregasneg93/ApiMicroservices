using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRequestClient<LoginUserCommand> _loginClient;

        public AuthController(IRequestClient<LoginUserCommand> loginClient)
        {
            _loginClient = loginClient;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _loginClient.GetResponse<LoginUserResponse>(
                                             new LoginUserCommand(request.Login, request.Password));

            if (!response.Message.Success)
            {
                return Unauthorized(new LoginResponse
                {
                    Success = false,
                    Message = response.Message.Message,
                    Role = null
                });
            }

            return Ok(new LoginResponse
            {
                Success = true,
                Message = response.Message.Message,
                Role = response.Message.Role
            });
        }
    }
}
