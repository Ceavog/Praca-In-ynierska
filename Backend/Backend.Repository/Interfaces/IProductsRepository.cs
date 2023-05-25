using Backend.DataAccessLibrary;
using Backend.Repository.GenericRepositories;

namespace Backend.Repository.Interfaces;

public interface IProductsRepository : IGenericRepository<Product>
{
    IEnumerable<Product> GetAllProductsByUserId(int id);
    Product UpdateProduct(Product product);
    bool CheckIfProductWithGivenNameAndUserIdAlreadyExists(int userId, string productName);
    bool CheckIfProductWithGivenIdExists(int productId);
    IEnumerable<Product> GetAllProductsByCategoryId(int categoryId);
    IEnumerable<Product> GetAllProductByOrderId(int orderId);
}