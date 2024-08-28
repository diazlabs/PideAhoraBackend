using Application.Products.Commands.CreateProduct;
using Application.Products.Commands.DeleteProduct;
using Application.Products.Commands.UpdateProduct;
using Application.Products.Queries.GetProductById;
using Application.Products.Queries.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]/{tenantId:guid}")]
    public class ProductsController(ISender _mediator) : ApiController
    {
        [HttpPost]
        public async Task<ActionResult> Create(Guid tenantId, CreateProductCommand command)
        {
            command.TenantId = tenantId;

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpPut("{productId:int}")]
        public async Task<ActionResult> Update(Guid tenantId, int productId, UpdateProductCommand command)
        {
            command.TenantId = tenantId;
            command.ProductId = productId;
            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpDelete("{productId:int}")]
        public async Task<ActionResult> Delete(Guid tenantId, int productId)
        {
            var command = new DeleteProductCommand(productId, tenantId);

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpGet("{productId:int}")]
        public async Task<ActionResult> GetProductById(Guid tenantId, int productId)
        {
            var command = new GetProductByIdQuery(productId, tenantId);

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts(Guid tenantId)
        {
            var command = new GetProductsQuery(tenantId);

            var products = await _mediator.Send(command);

            return Ok(products);
        }
    }
}
