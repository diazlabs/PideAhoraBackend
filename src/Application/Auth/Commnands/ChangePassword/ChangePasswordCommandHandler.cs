using Application.Common.Extensions;
using Application.Common.Interfaces;
using Ardalis.Result;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Auth.Commnands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result<ChangePasswordResponse>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        public ChangePasswordCommandHandler(UserManager<User> userManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<Result<ChangePasswordResponse>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.FindById(request.UserId);
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
