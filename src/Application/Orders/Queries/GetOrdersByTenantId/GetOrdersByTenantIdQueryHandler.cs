using Application.Common.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Queries.GetOrdersByTenantId
{
    internal class GetOrdersByTenantIdQueryHandler : IRequestHandler<GetOrdersByTenantIdQuery, List<GetOrdersByTenantIdResponse>>
    {
        private readonly ApplicationContext _context;
        public GetOrdersByTenantIdQueryHandler(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<List<GetOrdersByTenantIdResponse>> Handle(GetOrdersByTenantIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders
                .Include(x => x.OrderDetails)
                .ThenInclude(x => x.OrderDetailOptions)
                .ThenInclude(x => x.ChoiceOption)
                .Include(x => x.OrderDetails)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.ProductDiscounts)
                .Where(x => x.TenantId == request.TenantId)
                .Select(o => new GetOrdersByTenantIdResponse(
                    o.OrderId,
                    o.TenantId,
                    o.Name,
                    o.PhoneNumber,
                    o.Email,
                    o.UserId,
                    o.OrderNotes,
                    o.DeliveryType,
                    o.DeliveryAddress,
                    o.DeliveryDate,
                    o.DeliveryNotes,
                    o.Status,
                    o.Total,
                    o.OrderDetails.Select(od => new OrderDetailDto(
                        od.OrderDetailId,
                        od.OrderId,
                        od.ProductId,
                        od.Product.ProductName,
                        od.Product.Image,
                        od.ProductDiscount != null ? od.ProductDiscount.DiscountCode : null,
                        od.ProductDiscount != null ? od.ProductDiscount.Discount : null,
                        od.Quantity,
                        od.ProductPrice,
                        od.OrderDetailOptions.Select(option => new DetailOptionDto(
                            option.OrderDetailOptionId,
                            option.OrderDetailId,
                            option.OptionId,
                            option.ChoiceOption.Product.ProductName,
                            option.Quantity,
                            option.ProductPrice
                        ))
                    ))
                ))
                .ToListAsync(cancellationToken);

            return orders;
        }
    }
}
