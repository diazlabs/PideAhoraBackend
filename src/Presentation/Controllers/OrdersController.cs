using Application.Orders.Commands.CreateOrder;
using Application.Orders.Queries.GetOrdersByTenantId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class OrdersController(ISender _mediator) : ApiController
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> PlaceOrder(CreateOrderCommand command)
        {
            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpGet("{tenantId:Guid}")]
        public async Task<IActionResult> GetOrdersByTenantId(Guid tenantId)
        {
            var orders = await _mediator.Send(new GetOrdersByTenantIdQuery { TenantId = tenantId });

            return Ok(orders);
        }
    }
}
