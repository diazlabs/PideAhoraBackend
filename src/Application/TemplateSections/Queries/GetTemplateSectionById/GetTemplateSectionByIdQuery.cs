using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.TemplateSections.Queries.GetTemplateSectionById
{
    public class GetTemplateSectionByIdQuery : IRequest<Result<GetTemplateSectionByIdResponse>>
    {
        public Guid TenantId { get; set; }
        public Guid TenantTemplateId { get; set; }
        public int TemplateSectionId { get; set; }
    }

    public class GetTemplateSectionByIdValidator : AbstractValidator<GetTemplateSectionByIdQuery>
    {
        public GetTemplateSectionByIdValidator()
        {
            RuleFor(x => x.TenantId).RequireGuid();
            RuleFor(x => x.TenantTemplateId).RequireGuid();
            RuleFor(x => x.TemplateSectionId).GreaterThan(0).WithMessage("No es una seccion valida");
        }
    }
}
