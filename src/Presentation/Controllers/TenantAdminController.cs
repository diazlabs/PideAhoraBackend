using Application.Tenants.Commands.CreateTenant;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/admin/tenants")]
    public class TenantAdminController(ISender _mediator) : ApiController
    {
        [HttpPost]
        public async Task<ActionResult> Create(CreateTenantCommand command)
        {
            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }
    }
}
