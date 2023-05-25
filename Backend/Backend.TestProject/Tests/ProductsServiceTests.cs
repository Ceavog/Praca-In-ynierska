using Backend.DAL_EF;
using Backend.DataAccessLibrary;
using Backend.Repository;
using Backend.Repository.Interfaces;
using Backend.Repository.Repositories;
using Backend.Services;
using Backend.Services.Interface;
using Backend.Services.Services;
using Backend.Shared.Dtos.ProductDtos;
using Backend.Shared.Exceptions.ProductExceptions;
using Backend.Shared.Exceptions.UserExceptions;
using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Moq;

namespace Backend.TestProject;

[TestFixture]
public class ProductsServiceTests
{
    private IProductsRepository _productsRepository;
    private IUserRepository _userRepository;
    private ProductService _productService;
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var dbContext = InMemoryDbSeed.SeedDbContext();
        _productsRepository = new ProductsRepository(dbContext);
        _userRepository = new UserRepository(dbContext);
        _productService = new ProductService(_productsRepository, _userRepository);

    }

    [Test]
    public void GetAllProductsByUserId_ShouldThrowExceptionWhenUserDoesNotExists()
    {
        Action result = () => _productService.GetAllProductsByUserId(It.IsAny<int>());
        result.Should().ThrowExactly<UserWithGivenIdDoesNotExistsException>();
    }

    [Test]
    public void GetProductById_ShouldThrowExceptionWhenProductDoesNotExists()
    {
        Action result = () => _productService.GetProductById(It.IsAny<int>());
        result.Should().ThrowExactly<ProductWithGivenIdDoesNotExistsException>();
    }

    [Test]
    public void AddProduct_ThrowsExceptionWhenUserDoesNotExists()
    {
        var product = new AddProductDto()
        {
            UserId = It.IsAny<int>(),
        };
        Action result = () => _productService.AddProduct(product);
        result.Should().ThrowExactly<UserWithGivenIdDoesNotExistsException>();
    }

    [Test]
    public void AddProduct_ThrowsExceptionWhenProductAlreadyExists()
    {
        var product = new AddProductDto()
        {
            CategoryId = 1,
            UserId = 1,
            Name = "product1",
            Price = 100.0,
            Section = "section1",
        };

        Action result = () => _productService.AddProduct(product);
        result.Should().ThrowExactly<ProductWithThisNameAlreadyExistsForThisUserException>();
    }

    [Test]
    public void UpdateProduct_ThrowsExceptionWhenUserDoesNotExists()
    {
        var product = new UpdateProductDto()
        {
            UserId = It.IsAny<int>(),
        };
        Action result = () => _productService.UpdateProduct(product);
        result.Should().ThrowExactly<UserWithGivenIdDoesNotExistsException>();
    }

    [Test]
    public void UpdateProduct_ThrowsExceptionWhenProductAlreadyExistsForUser()
    {
        var product = new UpdateProductDto()
        {
            UserId = 1,
            Name = "product1"
        };
        Action result = () => _productService.UpdateProduct(product);
        result.Should().ThrowExactly<ProductWithThisNameAlreadyExistsForThisUserException>();
    }
    
    [Test]
    public void UpdateProduct_ThrowsExceptionWhenProductDoesNotExists()
    {
        var product = new UpdateProductDto()
        {
            Id = It.IsAny<int>(),
            UserId = 1
        };
        Action result = () => _productService.UpdateProduct(product);
        result.Should().ThrowExactly<ProductWithGivenIdDoesNotExistsException>();
    }

    [Test]
    public void DeleteProduct_ThrowsExceptionWhenProductDoesNotExists()
    {
        Action result = () => _productService.DeleteProduct(It.IsAny<int>());
        result.Should().ThrowExactly<ProductWithGivenIdDoesNotExistsException>();
    }
}