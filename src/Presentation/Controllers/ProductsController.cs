using Application.Products.Commands.CreateProduct;
using Application.Products.Commands.DeleteProduct;
using Application.Products.Commands.UpdateProduct;
using Application.Products.Queries.GetProductById;
using Application.Products.Queries.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class ProductsController(ISender _mediator) : ApiController
    {
        [HttpPost("{tenantId:Guid}")]
        public async Task<ActionResult> Create(Guid tenantId, CreateProductCommand command)
        {
            command.TenantId = tenantId;
            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpPut("{tenantId:Guid}")]
        public async Task<ActionResult> Update(Guid tenantId, UpdateProductCommand command)
        {
            command.TenantId = tenantId;
            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpDelete("{tenantId:Guid}")]
        public async Task<ActionResult> Delete(Guid tenantId, int productId)
        {
            var command = new DeleteProductCommand
            { 
                TenantId = tenantId,
                ProductId = productId,
                UserId = (Guid)UserId!
            };

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpGet("{tenantId:Guid}/product")]
        public async Task<ActionResult> GetProductById(Guid tenantId, int productId)
        {
            var command = new GetProductByIdQuery
            {
                TenantId = tenantId,
                ProductId = productId
            };

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpGet("{tenantId:Guid}")]
        public async Task<ActionResult> GetProducts(Guid tenantId)
        {
            var command = new GetProductsQuery
            {
                TenantId = tenantId
            };

            var products = await _mediator.Send(command);

            return Ok(products);
        }
    }
}
