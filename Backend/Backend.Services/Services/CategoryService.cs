using Backend.DataAccessLibrary;
using Backend.Repository.Interfaces;
using Backend.Services.Interface;
using Backend.Shared.Dtos;
using Backend.Shared.Dtos.CategoryDtos;
using Backend.Shared.Dtos.ProductDtos;
using Backend.Shared.Exceptions.CategoryExceptions;
using Backend.Shared.Exceptions.UserExceptions;
using Mapster;
using GetCategoryWithProductsDto = Backend.Shared.Dtos.ProductCategoryDtos.GetCategoryWithProductsDto;

namespace Backend.Services.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProductsRepository _productsRepository;
    public CategoryService(ICategoryRepository categoryRepository, IUserRepository userRepository, IProductsRepository productsRepository)
    {
        _categoryRepository = categoryRepository;
        _userRepository = userRepository;
        _productsRepository = productsRepository;
    }

    public AddCategoryDto AddCategory(AddCategoryDto addCategoryDto, UserDto user)
    {
        if (!_userRepository.CheckIfUserWithGivenIdExists(user.Id))
            throw new UserWithGivenIdDoesNotExistsException(user.Id);
        if (_categoryRepository.CheckIfCategoryWithGivenNameAlreadyExistsForUserWithGivenId(addCategoryDto.Name,
                user.Id))
            throw new CategoryWithGivenNameAndUserWithThisIdAlreadyExistsException(addCategoryDto.Name,
                user.Id);
        var category = addCategoryDto.Adapt<Category>();
        category.UserId = user.Id;
        return _categoryRepository.Add(category).Adapt<AddCategoryDto>();
    }

    public IEnumerable<GetCategoryDto> GetAllCategoriesForUserId(UserDto user)
    {
        //_userRepository.ThrowExceptionWhenUserWithGivenIdDoesNotExists(userId);
        if (!_userRepository.CheckIfUserWithGivenIdExists(user.Id))
            throw new UserWithGivenIdDoesNotExistsException(user.Id);
        return _categoryRepository.GetAllCategoriesForUserId(user.Id).Adapt<IEnumerable<GetCategoryDto>>();
    }

    public UpdateCategoryDto UpdateCategory(UpdateCategoryDto updateCategoryDto, UserDto user)
    {
        if (!_userRepository.CheckIfUserWithGivenIdExists(user.Id))
            throw new UserWithGivenIdDoesNotExistsException(user.Id);

        if (!_categoryRepository.CheckIfCategoryWithGivenIdExists(updateCategoryDto.Id))
            throw new CategoryWithGivenIdDoesNotExistsException(updateCategoryDto.Id);
        
        if (_categoryRepository.CheckIfCategoryWithGivenNameAlreadyExistsForUserWithGivenId(updateCategoryDto.Name,
                user.Id))
            throw new CategoryWithGivenNameAndUserWithThisIdAlreadyExistsException(updateCategoryDto.Name,
                user.Id);

        var categoryToUpdate = updateCategoryDto.Adapt<Category>();
        categoryToUpdate.UserId = user.Id;
        return _categoryRepository.UpdateCategory(categoryToUpdate).Adapt<UpdateCategoryDto>();
    }

    public CategoryDto DeleteCategory(int categoryId)
    {
        
        if (!_categoryRepository.CheckIfCategoryWithGivenIdExists(categoryId))
            throw new CategoryWithGivenIdDoesNotExistsException(categoryId);
        
        return _categoryRepository.Delete(categoryId).Adapt<CategoryDto>();
    }
/// <summary>
/// zwraca wszystkie kategorie usera
/// </summary>
/// <param name="user"></param>
/// <returns></returns>
/// <exception cref="UserWithGivenIdDoesNotExistsException"></exception>
    public IEnumerable<GetCategoryWithProductsDto> GetCategoryWithProducts(UserDto user)
    {
        if (!_userRepository.CheckIfUserWithGivenIdExists(user.Id))
            throw new UserWithGivenIdDoesNotExistsException(user.Id);
        
        var categoriesWithProducts = new List<GetCategoryWithProductsDto>();
        var categories = _categoryRepository.GetAllCategoriesForUserId(user.Id);
        foreach (var category in categories)
        {
            var categoryWithProduct = new GetCategoryWithProductsDto
            {
                Category = category.Adapt<CategoryDto>(),
                Products = _productsRepository.GetAllProductsByCategoryId(category.Id).Adapt<IEnumerable<ProductDto>>()
            };
            categoriesWithProducts.Add(categoryWithProduct);
        }

        return categoriesWithProducts;
    }
}