using Application.TenantTemplates.Commands.CreateTemplate;
using Application.TenantTemplates.Commands.DeleteTemplate;
using Application.TenantTemplates.Commands.UpdateTemplate;
using Application.TenantTemplates.Queries.GetTemplateById;
using Application.TenantTemplates.Queries.GetTemplates;
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

        [HttpPut("{TenantTemplateId:guid}")]
        public async Task<ActionResult> Update(Guid tenantId, Guid TenantTemplateId, UpdateTemplateCommand command)
        {
            command.TenantId = tenantId;
            command.TenantTemplateId = TenantTemplateId;

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpDelete]
        public async Task<ActionResult> Update(Guid tenantId, Guid TenantTemplateId)
        {
            var command = new DeleteTemplateCommand(tenantId, TenantTemplateId);

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpGet("{TenantTemplateId:int}")]
        public async Task<ActionResult> GetTemplateById(Guid tenantId, Guid TenantTemplateId)
        {
            var command = new GetTemplateByIdQuery(TenantTemplateId, tenantId);

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetTemplates(Guid tenantId)
        {
            var command = new GetTemplatesQuery(tenantId);

            var templates = await _mediator.Send(command);

            return Ok(templates);
        }
    }
}
