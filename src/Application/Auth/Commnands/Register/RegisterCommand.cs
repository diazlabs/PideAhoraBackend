using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.Auth.Commnands.Register
{
    public class RegisterCommand : IRequest<Result<RegisterCommandResponse>>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public int PhoneNumber { get; set; } = default!;
        public string Country { get; set; } = default!;
    }

    public class RegisterValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.FirstName).ValidateRequiredProperty("tu nombre", 2, 100);
            RuleFor(x => x.LastName).ValidateRequiredProperty("tu apellido", 2, 100);
            RuleFor(x => x.Email).ValidateEmail();
            RuleFor(x => x.PhoneNumber.ToString()).ValidatePhoneNumber();
            RuleFor(x => x.Password).ValidatePassword();
            RuleFor(x => x.Country).ValidateCountry();
        }
    }
}
