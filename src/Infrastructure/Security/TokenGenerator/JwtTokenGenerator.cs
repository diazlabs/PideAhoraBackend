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
            IEnumerable<string> tenants,
            List<Claim> claims,
            IEnumerable<string> roles)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            claims.Add(new(ClaimTypes.Name, firstName));
            claims.Add(new(ClaimTypes.Surname, lastName));
            claims.Add(new(ClaimTypes.Email, email));
            claims.Add(new(ClaimTypes.NameIdentifier, UserId.ToString()));
        
            claims.AddRange(tenants.Select(tenant => new Claim("tenant", tenant)));
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

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
