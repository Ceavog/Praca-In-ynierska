using Backend.DataAccessLibrary;
using Backend.Repository.Interfaces;
using Backend.Services.Interface;
using Backend.Shared.Dtos;
using Backend.Shared.Dtos.ProductDtos;
using Backend.Shared.Exceptions.ProductExceptions;
using Backend.Shared.Exceptions.UserExceptions;
using Mapster;

namespace Backend.Services.Services;

public class ProductService : IProductService
{
    private readonly IProductsRepository _productsRepository;
    private readonly IUserRepository _userRepository;
    public ProductService(IProductsRepository productsRepository, IUserRepository userRepository)
    {
        _productsRepository = productsRepository;
        _userRepository = userRepository;
    }

    public IEnumerable<GetProductDto> GetAllProductsByUserId(UserDto user)
    {
        
        if (!_userRepository.CheckIfUserWithGivenIdExists(user.Id))
            throw new UserWithGivenIdDoesNotExistsException(user.Id);
        
        return _productsRepository.GetAllProductsByUserId(user.Id).Adapt<IEnumerable<GetProductDto>>();
    }

    public GetProductDto GetProductById(int id)
    {
        if (!_productsRepository.CheckIfProductWithGivenIdExists(id))
            throw new ProductWithGivenIdDoesNotExistsException(id);
        
        return _productsRepository.Get(id)!.Adapt<GetProductDto>();
    }

    public AddProductDto AddProduct(AddProductDto productDto,  UserDto user)
    {
        // if (!_userRepository.CheckIfUserWithGivenIdExists(productDto.UserId))
        //     throw new UserWithGivenIdDoesNotExistsException(productDto.UserId);
        // if (_productsRepository.CheckIfProductWithGivenNameAndUserIdAlreadyExists(productDto.UserId, productDto.Name))
        //     throw new ProductWithThisNameAlreadyExistsForThisUserException(productDto.UserId, productDto.Name);

        var productToAdd = productDto.Adapt<Product>();
        productToAdd.UserId = user.Id;
        return _productsRepository.Add(productToAdd).Adapt<AddProductDto>();
    }
    public UpdateProductDto UpdateProduct(UpdateProductDto productDto,  UserDto user)
    {
        // if (!_userRepository.CheckIfUserWithGivenIdExists(productDto.UserId))
        //     throw new UserWithGivenIdDoesNotExistsException(productDto.UserId);
        // if (_productsRepository.CheckIfProductWithGivenNameAndUserIdAlreadyExists(productDto.UserId, productDto.Name))
        //     throw new ProductWithThisNameAlreadyExistsForThisUserException(productDto.UserId, productDto.Name);
        // if (!_productsRepository.CheckIfProductWithGivenIdExists(productDto.Id))
        //     throw new ProductWithGivenIdDoesNotExistsException(productDto.Id);
        
        var productToUpdate = productDto.Adapt<Product>();
        productToUpdate.UserId = user.Id;

        return _productsRepository.UpdateProduct(productDto.Adapt<Product>()).Adapt<UpdateProductDto>();
    }
    public DeleteProductDto DeleteProduct(int id)
    {
        if (!_productsRepository.CheckIfProductWithGivenIdExists(id))
            throw new ProductWithGivenIdDoesNotExistsException(id);
        return _productsRepository.Delete(id).Adapt<DeleteProductDto>();
    }
}