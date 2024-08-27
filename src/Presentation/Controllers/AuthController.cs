using Application.Auth.Commnands.ChangePassword;
using Application.Auth.Commnands.Login;
using Application.Auth.Commnands.Register;
using Application.Auth.Commnands.ResetPassword;
using Application.Auth.Commnands.SendResetPassword;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Contracts.Auth;

namespace Presentation.Controllers
{
    [AllowAnonymous]
    public class AuthController(ISender _mediator) : ApiController
    {
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            var command = new LoginCommand(request.Username, request.Password);

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterRequest request)
        {
            var command = new RegisterCommand(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password,
                request.PhoneNumber,
                request.Country);

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<ActionResult> ChnagePassword(ChangePasswordRequest request)
        {
            var command =  new ChangePasswordCommand(
                request.OldPassword,
                request.NewPassword,
                request.ConfirmPassword,
                request.UserId);

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPasswrod(ResetPasswordRequest request)
        {
            var command = new ResetPasswordCommand(
                request.UserId,
                request.Password,
                request.Token);

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpGet("send-reset-password")]
        public async Task<ActionResult> SendResetPassword(SendResetPasswordCommand command)
        {
            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }
    }
}
