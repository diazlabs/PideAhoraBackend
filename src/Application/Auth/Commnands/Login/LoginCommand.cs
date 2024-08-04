using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.Auth.Commnands.Login
{
    public class LoginCommand : IRequest<Result<LoginCommandResponse>>
    {
        public string UsernameOrEmail { get; set; } = default!;
        public string Password { get; set; } = default!;
    }

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
