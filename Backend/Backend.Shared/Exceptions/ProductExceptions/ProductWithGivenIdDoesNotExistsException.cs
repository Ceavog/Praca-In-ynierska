namespace Backend.Shared.Exceptions.ProductExceptions;

public class ProductWithGivenIdDoesNotExistsException : Exception
{
    public int Id { get; }
    public ProductWithGivenIdDoesNotExistsException(int id)
        : base($"Product with given id {id} does not exists")
    {
        Id = id;
    }
}