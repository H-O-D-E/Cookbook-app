using Microsoft.AspNetCore.Identity;

namespace Cookbook_app.Services;

public interface IJwtService
{
    string CreateToken(IdentityUser user);
}