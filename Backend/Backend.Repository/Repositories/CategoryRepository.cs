using Backend.DAL_EF;
using Backend.DataAccessLibrary;
using Backend.Repository.GenericRepositories;
using Backend.Repository.Interfaces;
using Backend.Shared.Exceptions.CategoryExceptions;

namespace Backend.Repository.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    public CategoryRepository(ApplicationDbContext applicationDbContext) 
        : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    
    public IEnumerable<Category> GetAllCategoriesForUserId(int userId)
    {
        return _applicationDbContext.Categories.Where(x => x.UserId.Equals(userId));
    }

    /// <summary>
    /// returns true if category exists
    /// </summary>
    /// <param name="categoryId"></param>
    /// <returns></returns>
    public bool CheckIfCategoryWithGivenIdExists(int categoryId)
    {
        return _applicationDbContext.Categories.Any(x => x.Id.Equals(categoryId));
    }

    public Category UpdateCategory(Category category)
    {
        _applicationDbContext.ChangeTracker.Clear();
        var updatedCategory = _applicationDbContext.Categories.Update(category).Entity;
        _applicationDbContext.SaveChanges();
        return updatedCategory;
    }
 
    /// <summary>
    /// returns true if category exists otherwise false
    /// </summary>
    /// <param name="categoryName"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public bool CheckIfCategoryWithGivenNameAlreadyExistsForUserWithGivenId(string categoryName, int userId)
    {
        return _applicationDbContext.Categories.Any(x => x.Name.Equals(categoryName) && x.UserId.Equals(userId));
    }
}