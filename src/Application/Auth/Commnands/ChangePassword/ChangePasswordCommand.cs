using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.Auth.Commnands.ChangePassword
{
    public class ChangePasswordCommand : IRequest<Result<ChangePasswordResponse>>
    {
        public string OldPassword { get; set; } = default!;
        public string NewPassword { get; set; } = default!;
        public string ConfirmPassword { get; set; } = default!;
        public Guid UserId { get; set; } = default!;
    }

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
