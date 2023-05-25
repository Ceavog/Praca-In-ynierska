using Backend.Shared.Dtos;
using Backend.Shared.Dtos.CategoryDtos;
using GetCategoryWithProductsDto = Backend.Shared.Dtos.ProductCategoryDtos.GetCategoryWithProductsDto;

namespace Backend.Services.Interface;

public interface ICategoryService
{
    AddCategoryDto AddCategory(AddCategoryDto addCategoryDto, UserDto user);
    IEnumerable<GetCategoryDto> GetAllCategoriesForUserId(UserDto user);
    UpdateCategoryDto UpdateCategory(UpdateCategoryDto updateCategoryDto, UserDto user);
    CategoryDto DeleteCategory(int categoryId);
    IEnumerable<GetCategoryWithProductsDto> GetCategoryWithProducts(UserDto user);
}