using Application.Common.Persistence;
using Ardalis.Result;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ProductChoices.Commands.DeleteChoice
{
    public class DeleteChoiceCommandHandler : IRequestHandler<DeleteChoiceCommand, Result<DeleteChoiceResponse>>
    {
        private readonly ApplicationContext _context;
        public DeleteChoiceCommandHandler(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Result<DeleteChoiceResponse>> Handle(DeleteChoiceCommand request, CancellationToken cancellationToken)
        {
            ProductChoice? productChoice = await _context.ProductChoices
                .Where(x => x.ProductChoiceId == request.ProductChoiceId && x.Product.TenantId == request.TenantId)
                .FirstOrDefaultAsync(cancellationToken);

            if (productChoice == null)
            {
                return Result.NotFound();
            }

            _context.ProductChoices.Remove(productChoice);

            int rows = await _context.SaveChangesAsync(cancellationToken);
            if (rows > 0)
            {
                return new DeleteChoiceResponse();
            }

            return Result.Error("No se pudo eliminar la elección");
        }
    }
}
