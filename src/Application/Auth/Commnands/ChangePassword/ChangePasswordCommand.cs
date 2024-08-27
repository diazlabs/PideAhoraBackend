using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.Auth.Commnands.ChangePassword
{
    public record ChangePasswordCommand(
        string OldPassword,
        string NewPassword,
        string ConfirmPassword,
        Guid UserId) : IRequest<Result<ChangePasswordResponse>>;

    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.UserId).RequireGuid();
            RuleFor(x => x.OldPassword).ValidateRequiredProperty("contraseña");
            RuleFor(x => x.NewPassword).ValidatePassword();
            RuleFor(x => x.ConfirmPassword)
                .ValidatePassword()
                .Must((model, field) => model.NewPassword == field )
                .WithMessage("La contraseña nueva debe ser igual");
        }
    }
}
