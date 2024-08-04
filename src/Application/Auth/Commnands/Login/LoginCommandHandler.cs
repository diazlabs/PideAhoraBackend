using Ardalis.Result;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Auth.Commnands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginCommandResponse>>
    {
        private readonly SignInManager<User> _signInManager;
        public LoginCommandHandler(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<Result<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _signInManager.UserManager.FindByEmailAsync(request.UsernameOrEmail);
            if (user == null)
            {
                user = await _signInManager.UserManager.FindByNameAsync(request.UsernameOrEmail);

                if (user == null) 
                {
                    return Result.Error("Usuario o contreseña incorrecto");
                }
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded)
            {
                return new LoginCommandResponse
                {
                    Token = "token"
                };
            }

            return Result.Error("Usuario o contreseña incorrecto");
        }
    }
}
