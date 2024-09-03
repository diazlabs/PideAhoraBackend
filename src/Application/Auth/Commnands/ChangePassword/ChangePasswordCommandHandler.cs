using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Security;
using Ardalis.Result;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Auth.Commnands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result<ChangePasswordResponse>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly ICurrentUserProvider _currentUserProvider;
        public ChangePasswordCommandHandler(UserManager<User> userManager, IUserService userService, ICurrentUserProvider currentUserProvider)
        {
            _userManager = userManager;
            _userService = userService;
            _currentUserProvider = currentUserProvider;
        }

        public async Task<Result<ChangePasswordResponse>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserProvider.GetUserId();
            User? user = await _userService.FindById(userId);
            if (user == null)
            {
                return Result.NotFound();
            }

            IdentityResult result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            if (result.Succeeded)
            {
                return new ChangePasswordResponse();
            }

            return result.ToErrorResult();
        }
    }
}
