using Backend.Shared.Dtos;
using Backend.Shared.Dtos.OrderDtos;

namespace Backend.Services.Interface;

public interface IOrderService
{
    void AddOrder(AddOrderDto addOrderDto, UserDto user);
    IEnumerable<OrderDto> GetTodayOrder(int userId);

}