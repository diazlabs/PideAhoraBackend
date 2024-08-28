using Application.Common.Interfaces;
using Ardalis.Result;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Auth.Commnands.SendResetPassword
{
    public class SendResetPasswordCommandHandler : IRequestHandler<SendResetPasswordCommand, Result>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        public SendResetPasswordCommandHandler(UserManager<User> userManager, IUserService userService, IEmailService emailService)
        {
            _userManager = userManager;
            _userService = userService;
            _emailService = emailService;
        }

        public async Task<Result> Handle(SendResetPasswordCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userService.FindByEmail(request.Email);

            if (user == null)
            {
                return Result.NotFound();
            }

            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _emailService.SendResetPasswordEmail(request.Email, token);

            return result;
        }
    }
}
