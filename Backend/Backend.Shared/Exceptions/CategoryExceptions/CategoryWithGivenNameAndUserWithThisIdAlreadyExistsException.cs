namespace Backend.Shared.Exceptions.CategoryExceptions;

public class CategoryWithGivenNameAndUserWithThisIdAlreadyExistsException : Exception
{
    public int UserId { get; set; }
    public string CategoryName { get; set; }

    public CategoryWithGivenNameAndUserWithThisIdAlreadyExistsException(string categoryName, int userId) 
        : base($"category with given name: {categoryName} already exists for user with id: {userId}")
    {
        UserId = userId;
        CategoryName = categoryName;
    }
}