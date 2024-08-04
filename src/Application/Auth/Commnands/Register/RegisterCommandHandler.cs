using Application.Common.Interfaces;
using Ardalis.Result;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Auth.Commnands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<RegisterCommandResponse>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        public RegisterCommandHandler(UserManager<User> userManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<Result<RegisterCommandResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            Guid userId = Guid.NewGuid();
            User newUser = new()
            {
                Id = userId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Country = request.Country,
                PhoneNumber = request.PhoneNumber.ToString(),
                UserName = request.Email,
                Creator = userId,
            };

            string[] validationErrors = await _userRepository.IsUserInUse(newUser);
            if (validationErrors.Length != 0)
            {
                return Result.Conflict(validationErrors);
            }

            IdentityResult result = await _userManager.CreateAsync(newUser);
            if (result.Succeeded)
            {
                return new RegisterCommandResponse
                {
                };
            }

            IEnumerable<string> errors = result.Errors.Select(x => x.Description);
            return Result.Error(new ErrorList(errors));
        }
    }
}
