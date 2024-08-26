using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.TenantTemplates.Commands.UpdateTemplate
{
    public class UpdateTemplateCommand : IRequest<Result<UpdateTemplateResponse>>
    {
        public Guid TenantId { get; set; }
        public Guid TemplateId { get; set; }
        public string Header { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Logo { get; set; } = default!;
        public Guid Modifier { get; set; }
    }

    public class UpdateTemplateValidator : AbstractValidator<UpdateTemplateCommand>
    {
        public UpdateTemplateValidator()
        {
            RuleFor(x => x.TenantId).RequireGuid();
            RuleFor(x => x.TemplateId).RequireGuid();
            RuleFor(x => x.Header).ValidateRequiredProperty("el titulo del template");
            RuleFor(x => x.Description).ValidateRequiredProperty("la descripción del template");
            RuleFor(x => x.Logo).ValidateRequiredProperty("el logo");
            RuleFor(x => x.Modifier).RequireGuid();
        }
    }
}
