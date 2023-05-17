using System.Security.Claims;
using backend.DTOs;
using backend.Model;
using backend.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("login")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;
    private readonly IUserService _userService;

    public AuthController(IAuthService service, IUserService userService)
    {
        _service = service;
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserLoginDto userLoginDto)
    {
        User? user;
        try
        {
            user = _service.Login(userLoginDto.LoginName, userLoginDto.Password);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Unauthorized(e.Message);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.LoginName)
        };
        //TODO: add role list as claims

        var identity = new ClaimsIdentity(claims, "MyCookieAuth");
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync("MyCookieAuth", principal, new AuthenticationProperties()
        {
            IsPersistent = true
        });
        
        return Ok("Here should come all the data that we want to return");
    }
}