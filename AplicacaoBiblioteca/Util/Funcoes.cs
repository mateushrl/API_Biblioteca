using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AplicacaoBiblioteca.Util
{
    public static class Funcoes
    {
        public static string CriaJWTToken(IConfiguration configuration)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, ""),
                new Claim(ClaimTypes.Name, ""),
            };
            var jwtToken = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(configuration["ApplicationSettings:JWT_Secret"])
                        ),
                    SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
