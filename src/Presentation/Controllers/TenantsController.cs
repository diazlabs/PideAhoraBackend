using Application.Tenants.Commands.DeleteTenant;
using Application.Tenants.Commands.SetActiveTenant;
using Application.Tenants.Commands.UpdateTenant;
using Application.Tenants.Queries.GetTenantById;
using Application.Tenants.Queries.GetTenantsByUser;
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

            var response = new Response<IEnumerable<GetTenantsByUserResponse>>()
            {
                Ok = true,
                Data = tenants,
            };

            return Ok(response);
        }

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
