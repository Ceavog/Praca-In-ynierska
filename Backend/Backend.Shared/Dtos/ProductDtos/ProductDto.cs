using Backend.Shared.Dtos.CategoryDtos;

namespace Backend.Shared.Dtos.ProductDtos;

public class ProductDto
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string Section { get; set; }
    public int CategoryId { get; set; }
    public CategoryDto Category { get; set; }
}