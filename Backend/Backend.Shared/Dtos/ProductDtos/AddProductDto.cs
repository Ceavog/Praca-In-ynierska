namespace Backend.Shared.Dtos.ProductDtos;

public class AddProductDto
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string Section { get; set; }
    public int CategoryId { get; set; }
}