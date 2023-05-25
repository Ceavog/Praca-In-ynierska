namespace Backend.Shared.Exceptions.UserExceptions;

public class UserPasswordDoesNotMachException : Exception
{
    public UserPasswordDoesNotMachException() :base("password does not match")
    {
        
    }
}