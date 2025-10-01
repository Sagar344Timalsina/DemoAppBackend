using DemoAppBE.Domain;
using DemoAppBE.Shared;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DemoAppBE.Infrastructure.Security
{

    public class TokenProvider(IAppSettings appSettings) : ITokenProvider
    {
        public string CreateRefereshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public string CreateToken(User user)
        {
            string secretKey = appSettings.JWTSecret!;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var roleClaims = user.UserRoles.Select(ur => new Claim(ClaimTypes.Role, ur.Role.Name)).ToList();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName)
            };

            claims.AddRange(roleClaims);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(appSettings.JWTTimeOut),
                SigningCredentials = credentials,
                Issuer = appSettings.JWTIssuer,
                Audience = appSettings.JWTAudience
            };

            var handler = new JsonWebTokenHandler();

            string token = handler.CreateToken(tokenDescriptor);

            return token;
        }
    }
}
