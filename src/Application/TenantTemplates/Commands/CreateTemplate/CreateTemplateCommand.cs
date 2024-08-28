using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.TenantTemplates.Commands.CreateTemplate
{
    public class CreateTemplateCommand : IRequest<Result<CreateTemplateResponse>>
    {
        public Guid TenantId { get; set; }
        public string Header { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Logo { get; set; } = default!;
    }

    public class CreateTemplateValidator : AbstractValidator<CreateTemplateCommand>
    {
        public CreateTemplateValidator()
        {
            RuleFor(x => x.TenantId).RequireGuid();
            RuleFor(x => x.Header).ValidateRequiredProperty("el titulo del template");
            RuleFor(x => x.Description).ValidateRequiredProperty("la descripción del template");
            RuleFor(x => x.Logo).ValidateRequiredProperty("el logo");
        }
    }
}
