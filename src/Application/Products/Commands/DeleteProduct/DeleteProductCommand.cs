using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.Products.Commands.DeleteProduct
{
    public record DeleteProductCommand(int ProductId, Guid TenantId) : IRequest<Result<DeleteProductResponse>>;

    public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("No es un producto válido");
            RuleFor(x => x.TenantId).RequireGuid();
        }
    }
}
