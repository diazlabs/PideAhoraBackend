using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.Auth.Commnands.ConfirmEmail
{
    public record ConfirmEmailCommand(string Token, string Email) : IRequest<Result>;

    public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailCommand>
    {
        public ConfirmEmailValidator()
        {
            RuleFor(x => x.Token).NotEmpty().WithMessage("El link de confirmación ha expirado");
            RuleFor(x => x.email).NotEmpty().WithMessage("El email es requerido");
        }
    }
}
