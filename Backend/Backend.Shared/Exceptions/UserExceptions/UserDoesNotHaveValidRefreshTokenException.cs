namespace Backend.Shared.Exceptions.UserExceptions;

public class UserDoesNotHaveValidRefreshTokenException : Exception
{
    public int Id { get; set; }
    public UserDoesNotHaveValidRefreshTokenException(int id) : base("This user does not have valid refresh token")
    {
        Id = id;
    }
}