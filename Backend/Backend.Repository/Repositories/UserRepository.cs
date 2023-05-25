using Backend.DAL_EF;
using Backend.DataAccessLibrary;
using Backend.Repository.GenericRepositories;
using Backend.Repository.Interfaces;
using Backend.Shared.Exceptions.UserExceptions;

namespace Backend.Repository.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public User? GetUserByLogin(string login)
    {
        var user = _applicationDbContext.Users.FirstOrDefault(x => x.Login.Equals(login));
        return user;
    }

    public bool CheckIfLoginExists(string login)
    {
        return _applicationDbContext.Users.Any(x => x.Login.Equals(login));
    }

    /// <summary>
    /// returns true if user exists otherwise returns false
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public bool CheckIfUserWithGivenIdExists(int userId)
    {
        return _applicationDbContext.Users.Any(x => x.Id.Equals(userId));
    }
}