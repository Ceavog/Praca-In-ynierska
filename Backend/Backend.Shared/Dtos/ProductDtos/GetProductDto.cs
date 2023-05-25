using Backend.Shared.Dtos.CategoryDtos;

namespace Backend.Shared.Dtos.ProductDtos;

public class GetProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string Section { get; set; }
    public int CategoryId { get; set; }
    public GetCategoryDto Category { get; set; }
}