using LagoaTrading.Domain.Entities;
using LagoaTrading.Shared.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LagoaTrading.Server.Core.Securities
{
    public class SecurityManager
    {
        public static string GenerateToken(User user)
        {
            var textKey = CryptoHelper.CreateMD5(DateTime.Now.ToString());
            var key = Encoding.UTF8.GetBytes(textKey);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Hash, user.RollingHash)
                }),
                Expires = DateTime.Now.Date.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
