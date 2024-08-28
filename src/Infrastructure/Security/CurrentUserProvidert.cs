using Application.Common.Security;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Security
{
    public class CurrentUserProvider(IHttpContextAccessor _httpContextAccessor) : ICurrentUserProvider
    {
        public CurrentUser GetCurrentUser()
        {
            if (_httpContextAccessor.HttpContext == null)
            {
                throw new ArgumentNullException();
            }

            var id = Guid.Parse(GetSingleClaimValue(ClaimTypes.NameIdentifier));
            var tenants = GetClaimValues("tenant");
            var roles = GetClaimValues(ClaimTypes.Role);
            var firstName = GetSingleClaimValue(ClaimTypes.Name);
            var lastName = GetSingleClaimValue(ClaimTypes.Surname);
            var email = GetSingleClaimValue(ClaimTypes.Email);

            return new CurrentUser(id, firstName, lastName, email, tenants, roles);
        }

        public Guid GetUserId()
        {
            return Guid.Parse(GetSingleClaimValue(ClaimTypes.NameIdentifier));
        }

        private List<string> GetClaimValues(string claimType) =>
            _httpContextAccessor.HttpContext!.User.Claims
                .Where(claim => claim.Type == claimType)
                .Select(claim => claim.Value)
                .ToList();

        private string GetSingleClaimValue(string claimType) =>
            _httpContextAccessor.HttpContext!.User.Claims
                .Single(claim => claim.Type == claimType)
                .Value;
    }
}
