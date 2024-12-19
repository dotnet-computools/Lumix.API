using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Lumix.Application.Auth;
using Lumix.Core.DTOs;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Lumix.Infrastructure.Authenfication.Jwt;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public string GenerateAccessToken(UserDto user)
    {
        var claims = new[]
        {
            new Claim(CustomClaims.UserId, user.Id.ToString()),
            new Claim("Admin", "true")
        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(_options.ExpiresHours),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

	public RefreshTokenDto GenerateRefreshToken(Guid userId)
    {
        return new RefreshTokenDto
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            ExpiresAt = DateTime.UtcNow.AddDays(_options.RefreshTokenExpiresDays),
            CreatedAt = DateTime.UtcNow
        };
    }

	RefreshTokenDto IJwtProvider.GenerateRefreshToken(Guid userId)
	{
		throw new NotImplementedException();
	}
}