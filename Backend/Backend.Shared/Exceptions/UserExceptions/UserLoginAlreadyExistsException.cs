namespace Backend.Shared.Exceptions.UserExceptions;

public class UserLoginAlreadyExistsException : Exception
{
    public string Login { get; set; }
    public UserLoginAlreadyExistsException(string login)
        : base($"This user login already exists")
    {
        Login = login;
    }
}