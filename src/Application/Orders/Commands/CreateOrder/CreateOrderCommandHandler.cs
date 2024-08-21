using Application.Common.Persistence;
using Ardalis.Result;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<CreateOrderResponse>>
    { 
        private readonly ApplicationContext _context;
        public CreateOrderCommandHandler(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Result<CreateOrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            List<OrderDetail> orderDetails = [];

            Guid orderId = Guid.NewGuid();

            foreach(var detail in request.OrderDetails)
            {
                var product = await _context.Products
                    .Include(x => x.ProductDiscounts)
                    .FirstOrDefaultAsync(x => x.ProductId == detail.ProductId, cancellationToken);

                if (product == null) 
                {
                    return Result.NotFound("No se encontro uno de los productos");
                }

                var orderDetail = new OrderDetail
                {
                    OrderId = orderId,
                    ProductId = product.ProductId,
                    ProductPrice = product.ProductPrice,
                    Quantity = detail.Quantity,
                    ProductDiscountId = detail.ProductDiscountId,
                };

                if (detail.OrderDetailOptions != null)
                {
                    var selectedIds = detail.OrderDetailOptions.Select(x => x.OptionId);
                    var selectedOptions = await _context.ChoiceOptions.Where(x => selectedIds.Contains(x.ChoiceOptionId)).ToListAsync(cancellationToken);

                    var hasAllOptions = selectedOptions.All(s => selectedIds.Contains(s.ChoiceOptionId));

                    if (selectedIds.Count() != selectedOptions.Count || !hasAllOptions)
                    {
                        return Result.NotFound("No se encontro una de las opciones");
                    }

                    orderDetail.OrderDetailOptions = detail.OrderDetailOptions.Select(x => new OrderDetailOption
                    {
                        OptionId = x.OptionId,
                        Quantity = x.Quantity,
                        ProductPrice = selectedOptions.First(s => s.ChoiceOptionId == x.OptionId).OptionPrice,
                    }).ToList();
                }
            }

            Func<OrderDetailOption, double> detailSum = o => o.Quantity * o.ProductPrice;

            Order order = new()
            {
                Creator = request.UserId ?? Guid.Empty,
                PhoneNumber = request.PhoneNumber,
                OrderNotes = request.OrderNotes,
                DeliveryAddress = request.DeliveryAddress,
                DeliveryDate = request.DeliveryDate,
                DeliveryNotes = request.DeliveryNotes,
                DeliveryType = request.DeliveryType,
                Email = request.Email,
                OrderDetails = orderDetails,
                Name = request.Name,
                TenantId = request.TenantId,
                UserId = request.UserId,
                OrderId = orderId,
                Total = (double)orderDetails
                        .Sum(x => ((x.ProductPrice - x.ProductDiscountId ?? 0) * x.Quantity)
                            + x.OrderDetailOptions?.Sum(detailSum) ?? 0)
            };
            _context.Orders.Add(order);

            var rows = await _context.SaveChangesAsync(cancellationToken);
            if (rows > 0)
            {
                return new CreateOrderResponse();
            }

            return Result.Error("No se pudo crear la orden, intenta de nuevo.");
        }
    }
}
