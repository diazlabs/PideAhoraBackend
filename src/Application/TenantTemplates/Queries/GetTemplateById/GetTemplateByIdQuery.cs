using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.TenantTemplates.Queries.GetTemplateById
{
    public class GetTemplateByIdQuery : IRequest<Result<GetTemplateByIdResponse>>
    {
        public Guid TenantTemplateId { get; set; }
        public Guid TenantId { get; set; }
    }

    public class GetTemplateByIdValidator : AbstractValidator<GetTemplateByIdQuery>
    {
        public GetTemplateByIdValidator()
        {
            RuleFor(x => x.TenantTemplateId).RequireGuid();
            RuleFor(x => x.TenantId).RequireGuid();
        }
    }
}
