using Backend.Shared.Dtos.ProductDtos;

namespace Backend.Shared.Dtos.OrderDtos;

public class OrderDto
{
    public IEnumerable<ProductDto>? Products { get; set; }
    public DateTime OrderDate { get; set; }
    public int Id { get; set; }
}