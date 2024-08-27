using Application.TemplateSections.Commands.CreateSection;
using Application.TemplateSections.Commands.DeleteSection;
using Application.TemplateSections.Commands.UpdateSection;
using Application.TemplateSections.Queries.GetTemplateSections;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class SectionsContoller(ISender _mediator) : ApiController
    {
        [HttpPost("{tenantId:guid}")]
        public async Task<ActionResult> Create(Guid tenantId, CreateSectionCommand command)
        {
            command.Creator = (Guid)UserId!;
            command.TenantId = tenantId;

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpPut("{tenantId:guid}")]
        public async Task<ActionResult> Update(Guid tenantId, int templateSectionId, UpdateSectionCommand command)
        {
            command.Modifier = (Guid)UserId!;
            command.TenantId = tenantId;
            command.TemplateSectionId = templateSectionId;

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpDelete("{tenantId:guid}")]
        public async Task<ActionResult> Delete(Guid tenantId, int templateSectionId, Guid templateId)
        {
            var command = new DeleteSectionCommand();   
            command.DeletedBy = (Guid)UserId!;
            command.TenantId = tenantId;
            command.SectionId = templateSectionId;
            command.TemplateId = templateId;

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpGet("{tenantId:guid}")]
        public async Task<ActionResult> GetTemplateSections(Guid tenantId, Guid templateId)
        {
            var command = new GetTemplateSectionsQuery();
           
            command.TenantId = tenantId;
            command.TemplateId = templateId;

            var sections = await _mediator.Send(command);

            return Ok(sections);
        }
    }
}
