using Application.Products.Commands.CreateProduct;
using Application.Products.Commands.DeleteProduct;
using Application.Products.Commands.UpdateProduct;
using Application.Products.Queries.GetProductById;
using Application.Products.Queries.GetProducts;
using Application.Products.Queries.GetProductsExtra;
using Domain.Common;
using Domain.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ChoicesDto = Application.Products.Commands.CreateProduct.ChoicesDto;

namespace Presentation.Controllers
{
    [Route("api/[controller]/{tenantId:guid}")]
    public class ProductsController(ISender _mediator) : ApiController
    {
        [HttpPost]
        public async Task<ActionResult> Create(Guid tenantId, CreateProductCommand command)
        {
            command.TenantId = tenantId;

            var choices = Request.Form.FirstOrDefault(x => x.Key == "choices");

            if (choices.Value.Count > 0)
            {
                var value = choices.Value.ToString();
                value = value.Substring(0, value.Length - 1);
                command.Choices = JsonConvert.DeserializeObject<List<ChoicesDto>>(value);
            }   
            
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

            var response = new Response<IEnumerable<GetProductsResponse>>();

            response.Ok = true;
            response.Data = products;

            return Ok(response);
        }

        [Route("/api/[controller]/types")]
        [HttpGet]
        public ActionResult GetProductTypes()
        {
            var response = new Response<ProductType[]>();

            response.Ok = true;
            response.Data = ProductType.Types;

            return Ok(response);
        }

        [HttpGet("extras")]
        public async Task<ActionResult> GetProductsExtra(Guid tenantId)
        {
            var command = new GetProductsExtraQuery(tenantId);

            var products = await _mediator.Send(command);

            var response = new Response<IEnumerable<GetProductsExtraResponse>>();

            response.Ok = true;
            response.Data = products;

            return Ok(response);
        }
    }
}
