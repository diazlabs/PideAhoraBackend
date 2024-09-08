using Domain.Common;
using Domain.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    public class TenantsController(ISender _mediator) : ApiController
    {
        [AllowAnonymous]
        [HttpGet("categories")]
        public ActionResult GetCategories()
        {
            var response = new Response<TenantCategory[]>();
            response.Data = TenantCategory.Categories;
            response.Ok = true;

            return Ok(response);
        }
    }
}
