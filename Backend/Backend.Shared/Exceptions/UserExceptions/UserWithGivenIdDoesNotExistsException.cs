namespace Backend.Shared.Exceptions.UserExceptions;

public class UserWithGivenIdDoesNotExistsException : Exception
{
    public int Id { get; set; }
    public UserWithGivenIdDoesNotExistsException(int id)
        : base($"user with given id {id} does not exists")
    {
        Id = id;
    }
}