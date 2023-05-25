using Backend.Shared.Dtos.ProductDtos;

namespace Backend.Shared.Dtos.OrderDtos;

public class AddOrderDto
{
    public IEnumerable<int> ProductIds { get; set; }
}