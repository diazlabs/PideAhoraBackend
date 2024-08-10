using Application.Auth.Commnands.Login;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    public class AuthController(ISender _mediator) : ApiController
    {
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginCommand command)
        {
            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }
    }
}
