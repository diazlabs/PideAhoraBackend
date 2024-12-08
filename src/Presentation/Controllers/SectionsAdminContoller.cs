using Application.TemplateSections.Commands.CreateSection;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/admin/sections/{tenantId:guid}")]
    public class SectionsAdminController(ISender _mediator) : ApiController
    {
        [HttpPost]
        public async Task<ActionResult> Create(Guid tenantId, CreateSectionCommand command)
        {
            command.TenantId = tenantId;

            var result = await _mediator.Send(command);

            return ToActionResult(result);
        }
    }
}
