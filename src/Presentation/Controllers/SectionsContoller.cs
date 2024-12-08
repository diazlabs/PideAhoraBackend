using Application.TemplateSections.Commands.DeleteSection;
using Application.TemplateSections.Commands.UpdateSection;
using Application.TemplateSections.Queries.GetTemplateSectionById;
using Application.TemplateSections.Queries.GetTemplateSections;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]/{tenantId:guid}")]
    public class SectionsController(ISender _mediator) : ApiController
    {
        [HttpPut("{templateSectionId:int}")]
        public async Task<ActionResult> Update(Guid tenantId, int templateSectionId, UpdateSectionCommand command)
        {
            command.TenantId = tenantId;
            command.TemplateSectionId = templateSectionId;

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpDelete("{templateSectionId:int}")]
        public async Task<ActionResult> Delete(Guid tenantId, int templateSectionId, Guid tenantTemplateId)
        {
            var command = new DeleteSectionCommand();

            command.TenantId = tenantId;
            command.SectionId = templateSectionId;
            command.TenantTemplateId = tenantTemplateId;

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpGet("{tenantTemplateId:guid}/{sectionId:int}")]
        public async Task<ActionResult> GetSectionById(Guid tenantId, Guid tenantTemplateId, int sectionId)
        {
            var command = new GetTemplateSectionByIdQuery()
            {
                TemplateSectionId = sectionId,
                TenantId = tenantId,
                TenantTemplateId = tenantTemplateId
            };

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpGet("{tenantTemplateId:guid}")]
        public async Task<ActionResult> GetTemplateSections(Guid tenantId, Guid tenantTemplateId)
        {
            var command = new GetTemplateSectionsQuery();

            command.TenantId = tenantId;
            command.TenantTemplateId = tenantTemplateId;

            var sections = await _mediator.Send(command);

            var response = new Response<IEnumerable<GetTemplateSectionsResponse>>();

            response.Data = sections;
            response.Ok = true;

            return Ok(response);
        }
    }
}
