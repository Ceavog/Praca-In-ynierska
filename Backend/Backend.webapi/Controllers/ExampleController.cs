using Backend.Services.Interface;
using Backend.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Backend.webapi;

[ApiController]
//[Authorize]
public class ExampleController : Controller
{
    [HttpGet("example1")]
    [EnableCors("_myAllowSpecificOrigins")]
    public IActionResult GetSampleString1()
    {
        Response.Cookies.Append("X-Access-Token", "down", new CookieOptions() { HttpOnly = true });
        return Ok("example");
    }
    
    
    [HttpGet("example2")]
    [EnableCors("_myAllowSpecificOrigins")]
    //[Authorize]
    public IActionResult GetSampleString2(CancellationToken cancellationToken)
    {
        Thread.Sleep(5000);
        if (cancellationToken.IsCancellationRequested)
        {
            return Ok("kurwa");
        }
        return Ok("example");
    }
}