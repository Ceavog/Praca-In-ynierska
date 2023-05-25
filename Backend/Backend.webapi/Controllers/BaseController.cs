using Backend.DataAccessLibrary;
using Backend.Services.Interface;
using Backend.Shared.Dtos;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Backend.webapi;


[EnableCors("_myAllowSpecificOrigins")]
public class BaseController : Controller
{
    protected UserDto AuthUser { get; set; }
    private readonly IUserService _userService; 
    
    public BaseController(IUserService userService)
    {
        _userService = userService;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);
        Request.Cookies.TryGetValue("X-Access-Token", out var accessToken);
        AuthUser = _userService.GetAllDataAboutUser(accessToken);
    }
}