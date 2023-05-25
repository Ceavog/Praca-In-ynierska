namespace Backend.Shared.Exceptions.CategoryExceptions;

public class CategoryWithGivenIdDoesNotExistsException : Exception
{
    public int Id { get; set; }
    public CategoryWithGivenIdDoesNotExistsException(int id) : 
        base($"Category with given id {id} does not exists")
    {
        Id = id;
    }
}