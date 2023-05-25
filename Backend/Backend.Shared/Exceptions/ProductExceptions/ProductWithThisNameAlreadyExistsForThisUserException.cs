namespace Backend.Shared.Exceptions.ProductExceptions;

public class ProductWithThisNameAlreadyExistsForThisUserException : Exception
{
    public int Id { get; set; }
    public string ProductName { get; }
    
    public ProductWithThisNameAlreadyExistsForThisUserException(int id, string productName) 
        : base($"Product with name {productName} already exists for user with id {id}")
    {
        Id = id;
        ProductName = productName;
    }
}