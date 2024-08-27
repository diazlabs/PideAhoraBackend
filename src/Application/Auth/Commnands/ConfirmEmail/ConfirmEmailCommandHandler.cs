using Application.Common.Extensions;
using Ardalis.Result;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Auth.Commnands.ConfirmEmail
{
    internal class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Result>
    {
        private readonly UserManager<User> _userManager;

        public ConfirmEmailCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return Result.Error("El link de confirmación ha expirado");
            }

            IdentityResult result = await _userManager.ConfirmEmailAsync(user, request.Token);
            if (result.Succeeded)
            {
                return Result.Success();
            }

            return result.ToErrorResult();
        }
    }
}
