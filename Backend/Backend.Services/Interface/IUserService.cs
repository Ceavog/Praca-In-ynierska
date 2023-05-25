using Backend.DataAccessLibrary;
using Backend.Shared.Dtos;
using Backend.Shared.Dtos.UserDtos;

namespace Backend.Services.Interface;

public interface IUserService
{
    void RegisterUser(string login, string password);
    AuthenticatedUserResposeDto LoginUser(LoginUserDto user);
    AuthenticatedUserResposeDto RefreshToken(string refreshToken, string token);

    UserDto GetAllDataAboutUser(string token);

}