using Backend.Shared.Dtos;

namespace Backend.Shared.Exceptions.UserExceptions;

public class UserLoginDoesNotExistsException : Exception
{
    public string Login { get; set; }
    public UserLoginDoesNotExistsException(string login)
    :base($"User Login: {login} does not exists")
    {
        Login = login;
    }
}