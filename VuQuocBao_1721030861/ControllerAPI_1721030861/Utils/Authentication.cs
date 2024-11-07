using ControllerAPI_1721030861.Database.Models;
using ControllerAPI_1721030861.Repositories.First_Approach;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ControllerAPI_1721030861.Utils
{
    public class Authentication
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<UserApi> _userService;
        public Authentication(IConfiguration configuration, IRepository<UserApi> userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        public string GenerateAccessToken(UserApi user)
        {
            var guid = Guid.NewGuid().ToString();
            var authoClaims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("UserName", user.UserName!),
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Role,"Nekowo"), // option role name
                new Claim(JwtRegisteredClaimNames.Jti, guid),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!)
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

            var tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtAccount;

            try
            {
                jwtAccount = tokenHandler.ReadJwtToken(token);
            }
            catch (Exception)
            {
                return ""; // Invalid token format
            }

            var jwtAccountId = jwtAccount.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (string.IsNullOrEmpty(jwtAccountId)) return ""; // Handle case where Id is not present
            var user = await _userService.GetAsync(int.Parse(jwtAccountId));
            if (user == null) return ""; // Handle case where user is not found

            return GenerateAccessToken(user);
        }

        public bool IsTokenValid(string token)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKeyBytes = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);
            var securityKey = new SymmetricSecurityKey(secretKeyBytes);

            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true, // This will check for token expiration
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = securityKey
            };

            try
            {
                // Validate the token
                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsTokenExpired(string token)
        {
            var jwtToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
                return true; // Invalid token

            return jwtToken.ValidTo < DateTime.UtcNow;
        }
    }
}
