using System.Security.Claims;
using Events.ConfigurationOptions;
using Events.Models;
using Events.Service.IService;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Events.Service;

public class JwtService:IJwtService
{
    private readonly JwtOptions _jwtOptions;
    
    public JwtService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions?.Value ?? throw new ArgumentNullException(nameof(jwtOptions));
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.ASCII.GetBytes(_jwtOptions.Key);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256Signature,
            SecurityAlgorithms.Sha256Digest);
    }

    private List<Claim> GetClaims(User user)
    {
        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.UserName),
            new("FirstName",user.FirstName),
            new(ClaimTypes.Surname, user.LastName),
            new(ClaimTypes.Role,user.Role.Name)
        };
        return claims;
    }

    public string GenerateJwtTokenAsync(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        
        var token = new JwtSecurityToken(_jwtOptions.Issuer,
            _jwtOptions.Audience,
            GetClaims(user),
            null,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: GetSigningCredentials());
        return new JwtSecurityTokenHandler().WriteToken(token);

    }
}