using Ardalis.Result;
using Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ApiController : ControllerBase
    {
        public ActionResult ToActionResult<T>(Result<T> result)
        {
            var response = new Response<T>()
            {
                Data = result.Value,
                Ok = false,
                GeneralErrors = result.Errors,
            };

            if (result.IsSuccess || result.IsOk())
            {
                response.Ok = true;
                return Ok(response);
            }

            if (result.IsCreated())
            {
                response.Ok = true;
                return Created("", response);
            }

            if (result.IsForbidden() || result.IsNotFound())
            {
                return NotFound(response);
            }

            if (result.IsConflict())
            {
                return Conflict(response);
            }

            if (result.IsInvalid() || result.IsError())
            {
                return BadRequest(response);
            }

            if (result.IsUnauthorized())
            {
                return Unauthorized(response);
            }

            if (result.IsCriticalError())
            {
                return StatusCode(500, response);
            }

            return StatusCode(500);
        }
    }
}
