using Backend.DataAccessLibrary;
using Backend.Repository.Interfaces;
using Backend.Services.Interface;
using Backend.Shared.Dtos;
using Backend.Shared.Dtos.OrderDtos;
using Backend.Shared.Dtos.ProductDtos;
using Mapster;

namespace Backend.Services.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderPositionsRepository _orderPositionsRepository;
    private readonly IProductsRepository _productsRepository;

    public OrderService(IOrderRepository orderRepository, IOrderPositionsRepository orderPositionsRepository, IProductsRepository productsRepository)
    {
        _orderRepository = orderRepository;
        _orderPositionsRepository = orderPositionsRepository;
        _productsRepository = productsRepository;
    }

    public void AddOrder(AddOrderDto addOrderDto, UserDto user)
    {
        var orderId = _orderRepository.AddOrder(new Order()
        {
            OrderTime = DateTime.Now,
            OrderTypeId = 1,
            UserId = user.Id
        });

        var orderPositionsToAdd = new List<OrderPosition>();
        foreach (var productId in addOrderDto.ProductIds)
        {
            orderPositionsToAdd.Add(new OrderPosition()
            {
                OrderId = orderId,
                ProductId = productId,
                UserId = user.Id
            });
        }
        _orderPositionsRepository.AddOrderPositions(orderPositionsToAdd);
        
    }

    public IEnumerable<OrderDto> GetTodayOrder(int id)
    {
        var orderList = new List<OrderDto>();
        var ordersFromDb = _orderRepository.GetOrdersByUserId(id);

        foreach (var orderFromDb in ordersFromDb)
        {

            var orderProducts = new List<Product?>();
            foreach (var orderPosition in orderFromDb.OrderPositions)
            {
                orderProducts.Add(_productsRepository.Get(orderPosition.ProductId));
            }
            var order = new OrderDto()
            {
                Id = orderFromDb.Id,
                OrderDate = orderFromDb.OrderTime,
                Products = orderProducts.Adapt<IEnumerable<ProductDto>>()
            };
            
            orderList.Add(order);
        }

        return orderList;
    }
}