using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.TenantTemplates.Commands.UpdateTemplate
{
    public class UpdateTemplateCommand : IRequest<Result<UpdateTemplateResponse>>
    {
        public Guid TenantId { get; set; }
        public Guid TenantTemplateId { get; set; }
        public string Header { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public IFormFile? Logo { get; set; }
    }

    public class UpdateTemplateValidator : AbstractValidator<UpdateTemplateCommand>
    {
        public UpdateTemplateValidator()
        {
            RuleFor(x => x.TenantId).RequireGuid();
            RuleFor(x => x.TenantTemplateId).RequireGuid();
            RuleFor(x => x.Header).ValidateRequiredProperty("el titulo del template");
            RuleFor(x => x.Name).ValidateRequiredProperty("el nombre del template");
            RuleFor(x => x.Description).ValidateRequiredProperty("la descripción del template");
            RuleFor(x => x.Logo).ValidateImage();
        }
    }
}
