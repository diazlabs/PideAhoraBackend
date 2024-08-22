using Application.Common.Persistence;
using Ardalis.Result;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ProductChoices.Commands.UpdateChoice
{
    public class UpdateChoiceCommandHandler : IRequestHandler<UpdateChoiceCommand, Result<UpdateChoiceResponse>>
    {
        private readonly ApplicationContext _context;
        public UpdateChoiceCommandHandler(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Result<UpdateChoiceResponse>> Handle(UpdateChoiceCommand request, CancellationToken cancellationToken)
        {
            ProductChoice? productChoice =  await _context.ProductChoices
                .Where(x => x.ProductChoiceId == request.ProductChoiceId && x.Product.TenantId == request.TenantId)
                .FirstOrDefaultAsync(cancellationToken);

            if (productChoice == null)
            {
                return Result.NotFound();
            }

            productChoice.Required = request.Required;
            productChoice.Quantity = request.Quantity;
            productChoice.Choice = request.Choice;

            int rows = await _context.SaveChangesAsync(cancellationToken);
            if (rows > 0)
            {
                return new UpdateChoiceResponse();
            }

            return Result.Error("Error al actualizar la elección");
        }
    }
}
