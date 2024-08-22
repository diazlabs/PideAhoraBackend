using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<Result<GetProductByIdResponse>>
    {
        public int ProductId { get; set; }
        public Guid TenantId { get; set; }
    }

    public class GetProductByIdValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdValidator()
        {
            RuleFor(x => x.TenantId).RequireGuid();
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("No es un producto válido");
        }
    }
}
