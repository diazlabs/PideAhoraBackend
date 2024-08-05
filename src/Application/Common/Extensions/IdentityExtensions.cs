using Ardalis.Result;
using Microsoft.AspNetCore.Identity;

namespace Application.Common.Extensions
{
    public static class IdentityExtensions
    {
        public static Result ToErrorResult(this IdentityResult result)
        {
            IEnumerable<string> errors = result.Errors.Select(x => x.Description);
            return Result.Error(new ErrorList(errors));
        } 
    }
}
