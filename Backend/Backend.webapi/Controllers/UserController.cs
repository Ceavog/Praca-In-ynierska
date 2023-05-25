using Backend.Services.Interface;
using Backend.Shared.Dtos;
using Backend.Shared.Exceptions.UserExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Backend.webapi;

[EnableCors("_myAllowSpecificOrigins")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly int _authUserId;
    public UserController(IUserService userService)
    {
        _userService = userService;

    }
    
    [HttpPost("RegisterUser")]
    [EnableCors("_myAllowSpecificOrigins")]
    public IActionResult RegisterUser(string login, string password)
    {
        try
        {
            _userService.RegisterUser(login, password);
            return Ok();
        }
        catch (Exception e) when (e is UserLoginAlreadyExistsException)
        {
            return Conflict(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest();
        }

    }

    [HttpPost("LoginUser")]
    public IActionResult Login(string login, string password)
    {
        try
        {
            var authenticatedUserResponse = _userService.LoginUser(new LoginUserDto { Login = login, Password = password });
            Response.Cookies.Append("X-Access-Token", authenticatedUserResponse.AccessToken, new CookieOptions() { HttpOnly = true });
            Response.Cookies.Append("X-Refresh-Token", authenticatedUserResponse.RefreshToken, new CookieOptions() {HttpOnly = true});
            Response.Cookies.Append("logged_in", "true", new CookieOptions());
            return Ok();
        }
        catch (Exception e)
        {
            return Unauthorized();
        }
    }

    [HttpGet("RefreshToken")]
    public IActionResult RefreshToken()
    {
        try
        {
            var isAccessToken = !Request.Cookies.TryGetValue("X-Access-Token", out var accessToken);
            var isRefreshToken = !Request.Cookies.TryGetValue("X-Refresh-Token", out var refreshToken);
            if (isAccessToken && isRefreshToken) 
                return BadRequest("Cookies missing in request");
            var authenticatedUserResponse = _userService.RefreshToken(refreshToken, accessToken);
            Response.Cookies.Append("X-Access-Token", authenticatedUserResponse.AccessToken,
                new CookieOptions() { HttpOnly = true });
            Response.Cookies.Append("X-Refresh-Token", authenticatedUserResponse.RefreshToken,
                new CookieOptions() { HttpOnly = true });
            return Ok();
        }
        catch (Exception e)
        {
            return Unauthorized();
        }
    }

    [HttpGet("Identity")]
    [EnableCors("_myAllowSpecificOrigins")]
    [Authorize]
    public IActionResult GetIdentity()
    {
        try
        {
            var isAccessToken = Request.Cookies.TryGetValue("X-Access-Token", out var accessToken);
            if (!isAccessToken)
                return BadRequest("Cookies missing in request");
            
            var user = _userService.GetAllDataAboutUser(accessToken);
            user.Role = "auth";
            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
        
    }


    

}