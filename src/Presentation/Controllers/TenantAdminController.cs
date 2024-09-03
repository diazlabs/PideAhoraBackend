using Application.Tenants.Commands.CreateTenant;
using Application.Tenants.Commands.DeleteTenant;
using Application.Tenants.Commands.SetActiveTenant;
using Application.Tenants.Commands.UpdateTenant;
using Application.Tenants.Queries.GetTenantById;
using Application.Tenants.Queries.GetTenantsByUser;
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

        [HttpPut("{tenantId:guid}")]
        public async Task<ActionResult> Update(Guid tenantId, UpdateTenantCommand command)
        {
            command.TenantId = tenantId;

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpDelete("{tenantId:guid}")]
        public async Task<ActionResult> Delete(Guid tenantId)
        {
            var command = new DeleteTenantCommand(tenantId);

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpPatch("{tenantId:guid}")]
        public async Task<ActionResult> SetActiveTenant(Guid tenantId, Guid TenantTemplateId)
        {
            var command = new SetActiveTemplateCommand(tenantId, TenantTemplateId);

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpGet("{tenantId:guid}")]
        public async Task<ActionResult> GetTenantById(Guid tenantId)
        {
            var command = new GetTenantByIdQuery(tenantId);

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetTenants()
        {
            var command = new GetTenantsByUserQuery();

            var tenants = await _mediator.Send(command);

            return Ok(tenants);
        }
    }
}
