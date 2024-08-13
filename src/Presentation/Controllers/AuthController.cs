using Application.Auth.Commnands.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    public class AuthController(ISender _mediator, ILogger<AuthController> _logger) : ApiController
    {
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginCommand command)
        {
            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpGet("test")]
        public async Task<ActionResult> test()
        {
            _logger.LogInformation("testing");
            return Ok(new { text = "dasdasdasd"});
        }
    }
}
