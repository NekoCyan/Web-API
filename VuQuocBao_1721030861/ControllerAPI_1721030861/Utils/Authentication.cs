using ControllerAPI_1721030861.Database.Models;
using ControllerAPI_1721030861.Repositories.Simple;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ControllerAPI_1721030861.Utils
{
    public class Authentication
    {
        private readonly IConfiguration _configuration;
        private readonly IAccountService _accountService;
        public Authentication(IConfiguration configuration, IAccountService accountService)
        {
            _configuration = configuration;
            _accountService = accountService;
        }

        public string GenerateAccessToken(Account account)
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

        public async Task<string> RefreshAccessToken(string token)
        {
            if (!IsTokenValid(token)) return "";
            if (!IsTokenExpired(token)) return token;

            var JwtAccount = new JwtSecurityToken(token);
            var JwtAccountId = JwtAccount.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            var account = await _accountService.GetAsync(int.Parse(JwtAccountId));

            return GenerateAccessToken(account);
        }

        private bool IsTokenValid(string token)
        {
            JwtSecurityToken SecurityToken;
            try
            {
                SecurityToken = new JwtSecurityToken(token);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool IsTokenExpired(string token)
        {
            if (!IsTokenValid(token)) return false;

            JwtSecurityToken SecurityToken = new JwtSecurityToken(token);

            return SecurityToken.ValidTo < DateTime.UtcNow;
        }
    }
}
