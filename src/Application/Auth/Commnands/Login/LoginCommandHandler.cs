using Application.Common.Interfaces;
using Ardalis.Result;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Auth.Commnands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginCommandResponse>>
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ITenantService _tenantService;
        public LoginCommandHandler(SignInManager<User> signInManager, IJwtTokenGenerator jwtTokenGenerator, ITenantService tenantService)
        {
            _signInManager = signInManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _tenantService = tenantService;
        }

        public async Task<Result<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _signInManager.UserManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return Result.Error("Usuario o contreseña incorrecto");
            }

            if (!user.EmailConfirmed)
            {
                return Result.Error("Debes confirmar tu correo electrónico");
            }

            if (!user.PhoneNumberConfirmed)
            {
                return Result.Error("Debes confirmar tu número de teléfono");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded)
            {
                var tenants = await _tenantService.GetTenantsByUserId(user.Id);
                var roles = await _signInManager.UserManager.GetRolesAsync(user);
                var claims = await _signInManager.UserManager.GetClaimsAsync(user);

                var token = _jwtTokenGenerator.GenerateToken(
                    user.Id,
                    user.FirstName,
                    user.LastName,
                    user.Email!,
                    tenants.Select(x => x.TenantId.ToString()),
                    claims.ToList(),
                    roles
                );    
                return new LoginCommandResponse
                {
                    Token = token
                };
            }

            return Result.Error("Usuario o contreseña incorrecto");
        }
    }
}
