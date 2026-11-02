using Microsoft.Extensions.Options;
using CarRental.Application.Common.Interfaces;
using CarRental.Application.Common.Security;
using CarRental.Domain.Identity.Entities.Users;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace CarRental.Infrastructure.Auth;

public class JwtTokenProvider(IOptions<JwtSettings> options,
    IRefreshTokenGenerator refreshTokenGenerator) : IJwtTokenProvider
{
    private readonly JwtSettings _settings = options.Value;

    private readonly IRefreshTokenGenerator _refreshTokenGenerator = refreshTokenGenerator;

    public TokenResult GenerateToken(User user)
    {
        var issuer = _settings.Issuer;
        var audience = _settings.Audience;
        var expires = DateTime.UtcNow.AddMinutes(_settings.TokenExpirationInMinutes);
        var key = _settings.Key;
        var jwtId = Guid.NewGuid();

        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Jti, jwtId.ToString()),
            new (JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new (JwtRegisteredClaimNames.Email, user.Email),
            new (JwtRegisteredClaimNames.GivenName, user.FirstName),
            new (JwtRegisteredClaimNames.FamilyName, user.LastName)
        };

        foreach (var userRole in user.UserRoles)
            claims.Add(new(ClaimTypes.Role, userRole.Role.Name));

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = issuer,
            Audience = audience,
            Expires = expires,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                SecurityAlgorithms.HmacSha256Signature
            )
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();

        var securityToken = tokenHandler.CreateToken(descriptor);

        var refreshToken = _refreshTokenGenerator.GenerateRefreshToken();

        return new TokenResult
        (
            tokenHandler.WriteToken(securityToken),
            refreshToken,
            expires,
            jwtId
        );
    }
}