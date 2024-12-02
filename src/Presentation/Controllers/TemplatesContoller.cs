using Application.TenantTemplates.Commands.CreateTemplate;
using Application.TenantTemplates.Commands.DeleteTemplate;
using Application.TenantTemplates.Commands.UpdateTemplate;
using Application.TenantTemplates.Queries.GetTemplateById;
using Application.TenantTemplates.Queries.GetTemplates;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]/{tenantId:guid}")]
    public class TemplatesController(ISender _mediator) : ApiController
    {
        [HttpPost]
        public async Task<ActionResult> Create(Guid tenantId, CreateTemplateCommand command)
        {
            command.TenantId = tenantId;
            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpPut("{tenantTemplateId:guid}")]
        public async Task<ActionResult> Update(Guid tenantId, Guid tenantTemplateId, UpdateTemplateCommand command)
        {
            command.TenantId = tenantId;
            command.TenantTemplateId = tenantTemplateId;

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpDelete("{tenantTemplateId:guid}")]
        public async Task<ActionResult> Delete(Guid tenantId, Guid tenantTemplateId)
        {
            var command = new DeleteTemplateCommand(tenantId, tenantTemplateId);

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpGet("{tenantTemplateId:guid}")]
        public async Task<ActionResult> GetTemplateById(Guid tenantId, Guid tenantTemplateId)
        {
            var command = new GetTemplateByIdQuery(tenantTemplateId, tenantId);

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetTemplates(Guid tenantId)
        {
            var command = new GetTemplatesQuery(tenantId);

            var templates = await _mediator.Send(command);

            var response = new Response<IEnumerable<GetTemplatesResponse>>()
            {
                Ok = true,
                Data = templates,
            };

            return Ok(response);
        }
    }
}
