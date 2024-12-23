﻿using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.Auth.Commnands.ResetPassword
{
    public record ResetPasswordCommand(
        string Email,
        string Password,
        string Token) : IRequest<Result<ResetPasswordResponse>>;

    public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordValidator()
        {
            RuleFor(x => x.Password).ValidatePassword();
            RuleFor(x => x.Token).ValidateRequiredProperty("código", 220, 250);
            RuleFor(x => x.Email).ValidateRequiredProperty("email");
        }
    }
}
