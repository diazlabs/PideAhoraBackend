using Application.TenantConfigs.Commands.CreateTenantConfig;
using Application.TenantConfigs.Commands.DeleteTenantConfig;
using Application.TenantConfigs.Commands.UpdateTenantConfig;
using Application.TenantConfigs.Commands.UpdateTenantConfigs;
using Application.TenantConfigs.Queries.GeTenantConfigs;
using Domain.Common;
using Domain.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/tenant-configuration/{tenantId:guid}")]
    public class TenantConfigurationController(ISender _mediator) : ApiController
    {
        [HttpPost]
        public async Task<ActionResult> Create(Guid tenantId, CreateTenantConfigCommand command)
        {
            command.TenantId = tenantId;

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpPut]
        public async Task<ActionResult> Update(Guid tenantId, UpdateTenantConfigCommand command)
        {
            command.TenantId = tenantId;

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpPut("all")]
        public async Task<ActionResult> Update(Guid tenantId, UpdateTenantConfigsCommand command)
        {
            command.TenantId = tenantId;

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }


        [HttpDelete]
        public async Task<ActionResult> Delete(Guid tenantConfigId)
        {
            var command = new DeleteTenantConfigCommand();

            command.TenantConfigId = tenantConfigId;

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetTenantConfigs(Guid tenantId)
        {
            var command = new GetTenantConfigsQuery();

            command.TenantId = tenantId;

            var configs = await _mediator.Send(command);

            var response = new Response<IEnumerable<GetTenantConfigsResponse>>();

            response.Data = configs;
            response.Ok = true;

            return Ok(response);
        }

        [HttpGet("types")]
        public ActionResult GetConfigTypes()
        {
            var response = new Response<IEnumerable<TenantConfigTypes>>();

            response.Data = TenantConfigTypes.All;
            response.Ok = true;

            return Ok(response);
        }
    }
}
