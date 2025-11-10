using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Backend_Dotnet_Mottu.Domain.Entities;
using Backend_Dotnet_Mottu.Application.Configs;

namespace Backend_Dotnet_Mottu.Application.Services;

public class TokenService(JwtSettings jwtSettings) : ITokenService
{
    private readonly JwtSettings _jwtSettings = jwtSettings ?? throw new ArgumentNullException(nameof(jwtSettings));
    
    public string GenerateToken(User user)
    {
        var handler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var tokenDescription = new SecurityTokenDescriptor
        {
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddMinutes(10),
            Subject = GenerateClaims(user)
        };

        var jwt = handler.CreateToken(tokenDescription);
        return handler.WriteToken(jwt);
    }
    
    private static ClaimsIdentity GenerateClaims(User usuario)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, usuario.Email),
            new(ClaimTypes.Name, usuario.Name),
            new("admin", usuario.IsAdmin.ToString())
        };
        
        return new ClaimsIdentity(claims);
    }
}