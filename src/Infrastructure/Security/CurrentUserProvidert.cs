using Application.Common.Security;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
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

            var id = GetUserId();
            var tenants = GetClaimValues("tenant");
            var roles = GetClaimValues(ClaimTypes.Role);
            var firstName = GetSingleClaimValue(JwtRegisteredClaimNames.Name);
            var lastName = GetSingleClaimValue(JwtRegisteredClaimNames.FamilyName);
            var email = GetSingleClaimValue(JwtRegisteredClaimNames.Email);

            return new CurrentUser(id, firstName, lastName, email, tenants, roles);
        }

        public Guid GetUserId()
        {
            return Guid.Parse(GetSingleClaimValue("id"));
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
