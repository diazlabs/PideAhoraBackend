using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.Auth.Commnands.Login
{
    public record LoginCommand(string Email, string Password) : IRequest<Result<LoginCommandResponse>>;

    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .ValidateEmail();

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Por favor ingresa tu contraseña")
                .NotNull()
                .WithMessage("Por favor ingresa tu contraseña");
        }
    }
}
