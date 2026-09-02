using Cookbook_app.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Cookbook_app.Controllers;


[ApiController]
[Route("/api/auth")]
public class AuthController : ControllerBase
{

    private readonly UserManager<IdentityUser> _userManager;

    public AuthController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }


    [HttpPost("/register")]
    public async Task<IActionResult> Register(RegisterUserRequest registerRequest)
    {
        var newUser = new IdentityUser
        {
            UserName = registerRequest.Username,
            Email = registerRequest.Email
        };

        var result = await _userManager.CreateAsync(
            newUser, registerRequest.Password
        );

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok();

    }
    
}