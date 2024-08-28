using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Tenants.Commands.CreateTenant
{
    public class CreateTenantCommand : IRequest<Result<CreateTenantResponse>>
    {
        public string Name { get; set; } = default!;
        public string PageTitle { get; set; } = default!;
        public string Path { get; set; } = default!;
        public string Description { get; set; } = default!;
        public IFormFile? Logo { get; set; }
        public string Category { get; set; } = default!;
    }

    public class CreateTenantValidator : AbstractValidator<CreateTenantCommand>
    {
        public CreateTenantValidator()
        {
            RuleFor(x => x.PageTitle).ValidateRequiredProperty("el titúlo de la página", 2, 50);
            RuleFor(x => x.Path).ValidateRequiredProperty("el path de la página", 2, 50);
            RuleFor(x => x.Name).ValidateRequiredProperty("el nombre de la página");
            RuleFor(x => x.Category).ValidateTenantCategory();
            RuleFor(x => x.Description).ValidateRequiredProperty("la descripción de la página");
            RuleFor(x => x.Logo).Must(x => true).When(x => x.Logo != null);
        }
    }
}
