using Backend.DataAccessLibrary;
using Backend.Shared.Dtos;
using Backend.Shared.Dtos.ProductDtos;

namespace Backend.Services.Interface;

public interface IProductService
{
    IEnumerable<GetProductDto> GetAllProductsByUserId(UserDto user);
    GetProductDto GetProductById(int id);
    AddProductDto AddProduct(AddProductDto productDto, UserDto user);
    UpdateProductDto UpdateProduct(UpdateProductDto productDto, UserDto user);
    DeleteProductDto DeleteProduct(int id);





}