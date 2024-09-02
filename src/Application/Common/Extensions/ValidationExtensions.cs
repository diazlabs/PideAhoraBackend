using Domain.Common.Enums;
using FluentValidation;

namespace Application.Common.Extensions
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> ValidatePassword<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Matches(Regexs.PasswordRegex)
                .WithMessage("La contraseña debe poseer 8-16 caracteres, una letra, un simbolo, una mayúscula y una minúscula");
        }

        public static IRuleBuilderOptions<T, string> ValidateEmail<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .WithMessage("Por favor ingresa un correo electrónico válido")
                .NotNull()
                .WithMessage("Por favor ingresa un correo electrónico válido")
                .EmailAddress()
                .WithMessage("Por favor ingresa un correo electrónico válido");
        }

        public static IRuleBuilderOptions<T, string?> ValidatePhoneNumber<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .WithMessage("Por favor ingresa tu número de teléfono")
                .NotNull()
                .WithMessage("Por favor ingresa tu número de teléfono")
                .Matches(Regexs.PhoneRegex)
                .WithMessage("Por favor ingresa tu número de teléfono sin espacios ni guiones");
        }

        public static IRuleBuilderOptions<T, string> ValidateCountry<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Must(country => Country.Countries.Any(c => c.Code == country))
                .WithMessage("{PropertyValue} no es un país válido");
        }

        public static IRuleBuilderOptions<T, string> ValidateTenantCategory<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Must(category => TenantCategory.Categories.Any(c => c == category))
                .WithMessage("{PropertyValue} no es una categoría válido");
        }

        public static IRuleBuilderOptions<T, string?> ValidateRequiredProperty<T>
            (this IRuleBuilder<T, string?> ruleBuilder, string property, int min = 2, int max = 100)
        {
            return ruleBuilder
                .NotEmpty()
                .WithMessage("Este campo es requerido")
                .NotNull()
                .WithMessage("Este campo es requerido")
                .Length(min, max)
                .WithMessage($"Debes ingresar {property} de {min}-{max} caracteres");
        }

        public static IRuleBuilderOptions<T, Guid> RequireGuid<T>(this IRuleBuilder<T, Guid> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .WithMessage("Este campo es requerido")
                .NotNull()
                .WithMessage("Este campo es requerido");
        }

        public static IRuleBuilderOptions<T, double> PriceGuard<T>(this IRuleBuilder<T, double> ruleBuilder)
        {
            return ruleBuilder.GreaterThan(x => 0);
        }
    }
}
