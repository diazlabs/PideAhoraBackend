using Application.Common.Extensions;
using FluentValidation;
using MediatR;

namespace Application.Products.Queries.GetProductsExtra
{
    public record GetProductsExtraQuery(Guid TenantId) : IRequest<IEnumerable<GetProductsExtraResponse>>;


    public class GetProductsQueryValidator : AbstractValidator<GetProductsExtraQuery>
    {
        public GetProductsQueryValidator()
        {
            RuleFor(x => x.TenantId).RequireGuid();
        }
    }
}
