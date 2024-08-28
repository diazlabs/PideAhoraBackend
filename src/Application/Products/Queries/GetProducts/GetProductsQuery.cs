using Application.Common.Extensions;
using FluentValidation;
using MediatR;

namespace Application.Products.Queries.GetProducts
{
    public record GetProductsQuery(Guid TenantId) : IRequest<List<GetProductsResponse>>;

    public class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
    {
        public GetProductsQueryValidator()
        {
            RuleFor(x => x.TenantId).RequireGuid();
        }
    }
}
