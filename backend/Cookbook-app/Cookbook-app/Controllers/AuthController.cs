using Cookbook_app.Models.Auth;
using Cookbook_app.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = Cookbook_app.Models.Auth.LoginRequest;

namespace Cookbook_app.Controllers;


[ApiController]
[Route("/api/auth")]
public class AuthController : ControllerBase
{

    private readonly UserManager<IdentityUser> _userManager;
    private readonly IJwtService _jwtService;

    public AuthController(UserManager<IdentityUser> userManager, IJwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
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
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);

        if (user is null)
        {
            return Unauthorized();
        }

        var validPassword =
            await _userManager.CheckPasswordAsync(user, request.Password);

        if (!validPassword)
        {
            return Unauthorized();
        }

        var token = _jwtService.CreateToken(user);

        return Ok(new
        {
            token
        });
    }
    
}