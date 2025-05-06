using Microsoft.AspNetCore.Mvc;
using MediatR;
using ProjectTracker.Application.Dtos.Account;
using ProjectTracker.Application.Features.Command;

namespace ProjectTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var command = new CreateAccountCommand { registerDto = registerDto };
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault()?.Message ?? "An error occurred.");

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var command = new LogInCommand { LogInDto = loginDto };
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return Unauthorized(result.Errors.FirstOrDefault()?.Message ?? "Unauthorized.");

            return Ok(result);
        }
    }
}