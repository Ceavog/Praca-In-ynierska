using Backend.Shared.Dtos.CategoryDtos;
using Backend.Shared.Dtos.ProductDtos;

namespace Backend.Shared.Dtos.ProductCategoryDtos;

public class GetCategoryWithProductsDto
{
    public CategoryDto Category { get; set; }
    public IEnumerable<ProductDto> Products { get; set; }
    
}