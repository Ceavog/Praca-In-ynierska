using System.Security.AccessControl;

namespace Backend.Shared.Dtos.UserDtos;

public class AuthenticatedUserResposeDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}