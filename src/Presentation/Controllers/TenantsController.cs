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
    public class TenantsController(ISender _mediator) : ApiController
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
        public async Task<ActionResult> Update(Guid tenantId)
        {
            var command = new DeleteTenantCommand();
            command.TenantId = tenantId;
            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpPatch("{tenantId:guid}")]
        public async Task<ActionResult> SetActiveTenant(Guid tenantId, Guid templateId)
        {
            var command = new SetActiveTemplateCommand();
            command.TenantId = tenantId;
            command.TemplateId = templateId;

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpGet("{tenantId:guid}")]
        public async Task<ActionResult> GetTenantById(Guid tenantId)
        {
            var command = new GetTenantByIdQuery();
            command.TenantId = tenantId;

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetTenants()
        {
            var command = new GetTenantsByUserQuery();
            command.UserId = (Guid)UserId!;

            var tenants = await _mediator.Send(command);

            return Ok(tenants);
        }
    }
}
