using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<Result<DeleteProductResponse>>
    {
        public int ProductId { get; set; }
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
    }

    public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("No es un producto válido");
            RuleFor(x => x.UserId).RequireGuid();
        }
    }
}
