
using Backend.Services.Interface;
using Backend.Shared.Dtos;
using Backend.Shared.Dtos.OrderDtos;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Backend.webapi;

[ApiController]
//[Authorize]
public class OrderController  : BaseController
{
    private readonly IOrderService _orderService;
    public OrderController(
        IOrderService orderService,
        IUserService userService) : base(userService)
    {
        _orderService = orderService;
    }
    [HttpPost("AddOrder")]
    
    public IActionResult AddOrder([FromBody] AddOrderDto addOrderDto)
    {
        try
        {
            _orderService.AddOrder(addOrderDto, AuthUser);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("today-orders")]
    public ActionResult<IEnumerable<OrderDto>> GetTodayOrder()
    {
        try
        {
            return Ok(_orderService.GetTodayOrder(AuthUser.Id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}
