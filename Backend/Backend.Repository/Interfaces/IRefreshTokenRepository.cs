using Backend.DataAccessLibrary;
using Backend.Repository.GenericRepositories;

namespace Backend.Repository.Interfaces;

public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
{
    RefreshToken? GetRefreshTokenByUserId(int id);
    bool CheckIfUserHasValidRefreshToken(int userId, string refreshToken);
    bool CheckIfUserHasValidRefreshToken(int userId);
}