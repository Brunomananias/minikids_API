using API_Minikids.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Minikids.Services
{
    public class TokenService
    {
        public static object GenerateToken(Cliente cliente)
        {
            // Ensure the key is at least 128 bits (16 bytes)
            var keyBytes = Encoding.ASCII.GetBytes(Key.Secret);
            if (keyBytes.Length < 16)
            {
                throw new InvalidOperationException("The key must be at least 128 bits (16 bytes) long.");
            }

            var key = new SymmetricSecurityKey(keyBytes);

            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
            new Claim("clienteId", cliente.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);
            var tokenString = tokenHandler.WriteToken(token);

            return new
            {
                token = tokenString
            };
        }

    }
}
