using Backend.Repository.Interfaces;
using Backend.Repository.Repositories;
using Backend.Services.Services;
using Backend.Shared.Dtos.CategoryDtos;
using Backend.Shared.Exceptions.CategoryExceptions;
using Backend.Shared.Exceptions.UserExceptions;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Backend.TestProject;


[TestFixture]
public class CategoryServiceTests
{
        private IProductsRepository _productsRepository;
        private IUserRepository _userRepository;
        private ICategoryRepository _categoryRepository;
        private CategoryService _categoryService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var dbContext = InMemoryDbSeed.SeedDbContext();
            _productsRepository = new ProductsRepository(dbContext);
            _userRepository = new UserRepository(dbContext);
            _categoryRepository = new CategoryRepository(dbContext);
            _categoryService = new CategoryService(_categoryRepository, _userRepository, _productsRepository);
        }

        [Test]
        public void AddCategory_throwsExceptionWhenUserDoesNotExists()
        {
            var addCategoryDto = new AddCategoryDto()
            {
                UserId = It.IsAny<int>()
            };
            Action result = () => _categoryService.AddCategory(addCategoryDto);
            result.Should().ThrowExactly<UserWithGivenIdDoesNotExistsException>();
        }

        [Test]
        public void AddCategory_ThrowsExceptionWhenCategoryAlreadyExistsForUser()
        {
            var addCategoryDto = new AddCategoryDto()
            {
                UserId = 1,
                Name = "category1"
            };

            Action result = () => _categoryService.AddCategory(addCategoryDto);
            result.Should().ThrowExactly<CategoryWithGivenNameAndUserWithThisIdAlreadyExistsException>();
            
        }

        [Test]
        public void UpdateCategory_ThrowsExceptionWhenUserDoesNotExists()
        {
            var updateCategoryDto = new UpdateCategoryDto()
            {
                UserId = It.IsAny<int>()
            };
            Action result = () => _categoryService.UpdateCategory(updateCategoryDto);
            result.Should().ThrowExactly<UserWithGivenIdDoesNotExistsException>();
        }

        [Test]
        public void UpdateCategory_ThrowsExceptionWhenCategoryDoesNotExists()
        {
            var updateCategoryDto = new UpdateCategoryDto()
            {
                Id = It.IsAny<int>(),
                UserId = 1,
            };
            Action result = () => _categoryService.UpdateCategory(updateCategoryDto);
            result.Should().ThrowExactly<CategoryWithGivenIdDoesNotExistsException>();
        }

        [Test]
        public void UpdateCategory_ThrowsExceptionWhenCategoryExistsForUser()
        {
            var updateCategoryDto = new UpdateCategoryDto()
            {
                Id = 1,
                Name = "category1",
                UserId = 1
            };
            Action result = () => _categoryService.UpdateCategory(updateCategoryDto);
            result.Should().ThrowExactly<CategoryWithGivenNameAndUserWithThisIdAlreadyExistsException>();
        }

        [Test]
        public void GetAllCategoriesForUserId_ThrowsExceptionIfUserDoesNotExists()
        {
            Action result = () => _categoryService.GetAllCategoriesForUserId(It.IsAny<int>());
            result.Should().ThrowExactly<UserWithGivenIdDoesNotExistsException>();
        }

        [Test]
        public void DeleteCategory_ThrowsExceptionWhenCategoryDoesNotExists()
        {
            Action result = () => _categoryService.DeleteCategory(It.IsAny<int>());
            result.Should().ThrowExactly<CategoryWithGivenIdDoesNotExistsException>();
        }

        [Test]
        public void GetCategoriesWithProduct_ThrowsExceptionWhenUserDoesNotExists()
        {
            Action result = () => _categoryService.GetCategoryWithProducts(It.IsAny<int>());
            result.Should().ThrowExactly<UserWithGivenIdDoesNotExistsException>();
        }
}