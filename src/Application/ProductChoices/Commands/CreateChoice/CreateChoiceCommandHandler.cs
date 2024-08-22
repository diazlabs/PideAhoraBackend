using Application.Common.Interfaces;
using Application.Common.Persistence;
using Ardalis.Result;
using Domain.Entities;
using MediatR;

namespace Application.ProductChoices.Commands.CreateChoice
{
    public class CreateChoiceCommandHandler : IRequestHandler<CreateChoiceCommand, Result<CreateChoiceResponse>>
    {
        private readonly ApplicationContext _context;
        private readonly IProductService _productService;
        public CreateChoiceCommandHandler(ApplicationContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }
        public async Task<Result<CreateChoiceResponse>> Handle(CreateChoiceCommand request, CancellationToken cancellationToken)
        {
            bool productExist = await _productService.ProductExist(request.ProductId, request.TenantId);
            if (!productExist)
            {
                return Result.NotFound();
            }

            var newChoice = new ProductChoice
            {
                Choice = request.Choice,
                CreatedAt = DateTime.UtcNow,
                Creator = request.Creator,
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                Required = request.Required,
            };

            _context.ProductChoices.Add(newChoice);

            int rows = await _context.SaveChangesAsync();
            if (rows > 0 )
            {
                return new CreateChoiceResponse();
            }

            return Result.Error("Error al crear la elección");
        }
    }
}
