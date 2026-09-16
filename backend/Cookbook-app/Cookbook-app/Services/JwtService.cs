using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Cookbook_app.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateToken(IdentityUser user)
    {
        var issuer = _configuration["Jwt:ValidIssuer"];
        var audience = _configuration["Jwt:ValidAudience"];
        // REVIEW(noob): the null check on the signing key runs on every call, although Program.cs already proved at startup that it is set. That is the symptom of reading raw configuration keys instead of binding them once.
        var signingKey = _configuration["Jwt:IssuerSigningKey"]
                         ?? throw new InvalidOperationException(
                             "JWT signing key is not configured.");

        // REVIEW(sec): the token carries the user id and name but no roles, while Identity is configured with roles. If you add [Authorize(Roles = ...)] later it will silently never match, because the role claims are not in the token.
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName!)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(signingKey));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            // REVIEW(config): the one-hour lifetime is hardcoded while the issuer and audience come from IConfiguration right above it. Make it Jwt:ExpiryMinutes and read it the same way. Better still, bind the whole Jwt section to an options class with builder.Services.Configure<JwtOptions>(...) and inject IOptions<JwtOptions>, so the keys are validated once at startup instead of being re-read and re-checked on every single token.
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}