using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.Products.Queries.GetProducts
{
    public class GetProductsQuery : IRequest<List<GetProductsResponse>>
    {
        public Guid TenantId { get; set; }
    }

    public class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
    {
        public GetProductsQueryValidator()
        {
            RuleFor(x => x.TenantId).RequireGuid();
        }
    }
}
