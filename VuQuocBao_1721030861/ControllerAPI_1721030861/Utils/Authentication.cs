using ControllerAPI_1721030861.Database.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ControllerAPI_1721030861.Utils
{
    public class Authentication
    {
        public readonly IConfiguration _configuration;
        public Authentication(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private string GenerateAccessToken(Account account)
        {
            var guid = Guid.NewGuid().ToString();
            var authoClaims = new List<Claim>
            {
                new Claim("Id", account.Id.ToString()),
                new Claim("UserName", account.UserName!),
                new Claim(ClaimTypes.Name, account.UserName!),
                new Claim(ClaimTypes.Role,"Nekowo"), // option role name
                new Claim(JwtRegisteredClaimNames.Jti, guid),
                new Claim(JwtRegisteredClaimNames.Email, account.Email!)
            };

            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKeyBytes = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);
            var securityKey = new SymmetricSecurityKey(secretKeyBytes);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: authoClaims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
