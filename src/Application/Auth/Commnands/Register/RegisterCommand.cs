using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.Auth.Commnands.Register
{
    public record RegisterCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password,
        string PhoneNumber,
        string Country) : IRequest<Result<RegisterCommandResponse>>;

    public class RegisterValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.FirstName).ValidateRequiredProperty("tu nombre", 2, 100);
            RuleFor(x => x.LastName).ValidateRequiredProperty("tu apellido", 2, 100);
            RuleFor(x => x.Email).ValidateEmail();
            RuleFor(x => x.PhoneNumber).ValidatePhoneNumber();
            RuleFor(x => x.Password).ValidatePassword();
            RuleFor(x => x.Country).ValidateCountry();
        }
    }
}
