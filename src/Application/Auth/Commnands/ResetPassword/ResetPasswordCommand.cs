using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.Auth.Commnands.ResetPassword
{
    public record ResetPasswordCommand(
        Guid UserId,
        string Password,
        string Token) : IRequest<Result<ResetPasswordResponse>>;

    public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordValidator()
        {
            RuleFor(x => x.Password).ValidatePassword();
            RuleFor(x => x.Token).ValidateRequiredProperty("código");
            RuleFor(x => x.UserId).RequireGuid();
        }
    }
}
