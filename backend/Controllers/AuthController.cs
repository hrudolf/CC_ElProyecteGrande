using System.Security.Claims;
using backend.DTOs;
using backend.Model;
using backend.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;
    private readonly IEmployeeService _employeeService;

    public AuthController(IAuthService service, IEmployeeService employeeService)
    {
        _service = service;
        _employeeService = employeeService;
    }

    [Route("login")]
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
            return Unauthorized("{\"message\": " + "\"" + $"{e.Message}" + "\"}");
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username)
        };
        
        claims.Add(new Claim(ClaimTypes.Role, user.Role.ToString()));

        var identity = new ClaimsIdentity(claims, "MyCookieAuth");
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync("MyCookieAuth", principal, new AuthenticationProperties()
        {
            IsPersistent = true
        });

        if (user.Employee == null)
            return Ok(new UserLoginResponseDto()
            {
                Id = 0,
                FirstName = user.Username,
                LastName = "",
                Role = user.Role.ToString()
            });

        UserLoginResponseDto responseData = new UserLoginResponseDto()
        {
            Id = user.Employee.Id,
            FirstName = user.Employee.FirstName,
            LastName = user.Employee.LastName,
            Role = user.Role.ToString()
        };
        return Ok(responseData);
    }

    [Route("login")]
    [HttpGet]
    public IActionResult Login()
    {
        if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated && HttpContext.User.Identity.Name != null)
        {
            User user = _service.FindByUsername(HttpContext.User.Identity.Name);
            UserLoginResponseDto responseData = new UserLoginResponseDto()
            {
                Id = user.Employee?.Id ?? user.Id,
                FirstName = user.Employee?.FirstName ?? user.Username,
                LastName = user.Employee?.LastName ?? "",
                Role = user.Role.ToString()
            };
            return Ok(responseData);
        }
        return Unauthorized("{\"message\": " + "\"" + "User not found" + "\"}");
    }

    [Route("logout")]
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Ok();
    }
}