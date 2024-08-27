using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.Auth.Commnands.Login
{
    public record LoginCommand(string UsernameOrEmail, string Password) : IRequest<Result<LoginCommandResponse>>;

    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(x => x.UsernameOrEmail)
                .NotEmpty()
                .WithMessage("Por favor ingresa tu usuario o correo electrónico")
                .NotNull()
                .WithMessage("Por favor ingresa tu usuario o correo electrónico");

            RuleFor(x => x.UsernameOrEmail)
                .ValidateEmail()
                .When(x => x.UsernameOrEmail.Contains('@'));

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Por favor ingresa tu contraseña")
                .NotNull()
                .WithMessage("Por favor ingresa tu contraseña");
        }
    }
}
