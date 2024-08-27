using Application.TenantTemplates.Commands.CreateTemplate;
using Application.TenantTemplates.Commands.DeleteTemplate;
using Application.TenantTemplates.Commands.UpdateTemplate;
using Application.TenantTemplates.Queries.GetTemplateById;
using Application.TenantTemplates.Queries.GetTemplates;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class TemplatesContoller(ISender _mediator) : ApiController
    {
        [HttpPost("{tenantId:guid}")]
        public async Task<ActionResult> Create(Guid tenantId, CreateTemplateCommand command)
        {
            command.TenantId = tenantId;
            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpPut("{tenantId:guid}")]
        public async Task<ActionResult> Update(Guid tenantId, Guid templateId, UpdateTemplateCommand command)
        {
            command.TenantId = tenantId;
            command.TemplateId = templateId;
            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpDelete("{tenantId:guid}")]
        public async Task<ActionResult> Update(Guid tenantId, Guid templateId)
        {
            var command = new DeleteTemplateCommand();
            command.TenantId = tenantId;
            command.TemplateId = templateId;
            command.DeletedBy = (Guid)UserId!;

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpGet("{tenantId:guid}/template")]
        public async Task<ActionResult> GetTemplateById(Guid tenantId, Guid templateId)
        {
            var command = new GetTemplateByIdQuery();
            command.TenantId = tenantId;
            command.TenantTemplateId = templateId;

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpGet("{tenantId:guid}")]
        public async Task<ActionResult> GetTemplates(Guid tenantId)
        {
            var command = new GetTemplatesQuery();
            command.TenantId = tenantId;

            var templates = await _mediator.Send(command);

            return Ok(templates);
        }
    }
}
