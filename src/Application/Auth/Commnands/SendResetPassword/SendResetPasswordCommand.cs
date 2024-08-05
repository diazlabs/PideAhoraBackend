using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.Auth.Commnands.SendResetPassword
{
    public class SendResetPasswordCommand : IRequest<Result>
    {
        public string Email { get; set; } = default!;
    }

    public class SendResetPasswordValidator : AbstractValidator<SendResetPasswordCommand>
    {
        public SendResetPasswordValidator()
        {
            RuleFor(x => x.Email).ValidateEmail();
        }
    }
}
