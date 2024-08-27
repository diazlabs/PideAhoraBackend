using Application.Common.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Security.TokenGenerator
{
    public class JwtTokenGenerator(IOptions<JwtSettings> jwtOptions) : IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings = jwtOptions.Value;

        public string GenerateToken(
            Guid UserId,
            string firstName,
            string lastName,
            string email,
            List<string> tenants,
            List<string> permissions,
            List<string> roles)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Name, firstName),
            new(JwtRegisteredClaimNames.FamilyName, lastName),
            new(JwtRegisteredClaimNames.Email, email),
            new("id", UserId.ToString()),
        };

            roles.ForEach(role => claims.Add(new(ClaimTypes.Role, role)));
            permissions.ForEach(permission => claims.Add(new("permissions", permission)));
            tenants.ForEach(tenant => claims.Add(new("tenants", tenant)));

            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
