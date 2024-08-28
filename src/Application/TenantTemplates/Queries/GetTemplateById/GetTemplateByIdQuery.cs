using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.TenantTemplates.Queries.GetTemplateById
{
    public record GetTemplateByIdQuery(Guid TenantTemplateId, Guid TenantId) : IRequest<Result<GetTemplateByIdResponse>>;

    public class GetTemplateByIdValidator : AbstractValidator<GetTemplateByIdQuery>
    {
        public GetTemplateByIdValidator()
        {
            RuleFor(x => x.TenantTemplateId).RequireGuid();
            RuleFor(x => x.TenantId).RequireGuid();
        }
    }
}
