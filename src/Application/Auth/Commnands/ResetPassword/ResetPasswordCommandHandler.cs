using Application.Common.Extensions;
using Application.Common.Interfaces;
using Ardalis.Result;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Auth.Commnands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result<ResetPasswordResponse>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        public ResetPasswordCommandHandler(UserManager<User> userManager, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }

        public async Task<Result<ResetPasswordResponse>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userService.FindByEmail(request.Email);

            if (user == null)
            {
                return Result.NotFound();
            }

            IdentityResult result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            if (result.Succeeded)
            {
                return new ResetPasswordResponse();
            }

            return result.ToErrorResult();
        }
    }
}
