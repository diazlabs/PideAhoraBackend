using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.Auth.Commnands.ResetPassword
{
    public class ResetPasswordCommand : IRequest<Result<ResetPasswordResponse>>
    {
        public Guid UserId { get; set; }
        public string Password { get; set; } = default!;
        public string Token { get; set; } = default!;
    }

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
