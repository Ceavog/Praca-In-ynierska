using Backend.DataAccessLibrary;
using Backend.Repository.GenericRepositories;

namespace Backend.Repository.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    User? GetUserByLogin(string login);
    bool CheckIfLoginExists(string login);
    bool CheckIfUserWithGivenIdExists(int userId);
}