using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.Products.Queries.GetProductById
{
    public record GetProductByIdQuery(int ProductId, Guid TenantId) : IRequest<Result<GetProductByIdResponse>>;

    public class GetProductByIdValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdValidator()
        {
            RuleFor(x => x.TenantId).RequireGuid();
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("No es un producto válido");
        }
    }
}
