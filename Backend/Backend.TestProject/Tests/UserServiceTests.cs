using Backend.Repository.Interfaces;
using Backend.Repository.Repositories;
using Backend.Services;
using Backend.Services.Services;
using Backend.Shared.Dtos;
using Backend.Shared.Exceptions.UserExceptions;
using FluentAssertions;
using Moq;

namespace Backend.TestProject;

[TestFixture]
public class UserServiceTests
{
    private IJwtConfiguration _jwtConfiguration;
    private IUserRepository _userRepository;
    private IRefreshTokenRepository _refreshTokenRepository;
    private UserService _userService;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _jwtConfiguration = new JwtConfiguration()
        {
            Issuer = "https://localhost:7161/",
            Key = "ThisismySecretKey",
        };
        var dbContext = InMemoryDbSeed.SeedDbContext();
        _userRepository = new UserRepository(dbContext);
        _refreshTokenRepository = new RefreshTokenRepository(dbContext);
        _userService = new UserService(_jwtConfiguration, _userRepository, _refreshTokenRepository);
    }

    [Test]
    public void RegisterUser_ThrowsExceptionWhenUserLoginAlreadyExists()
    {
        var userLogin = "1234";
        var password = Guid.NewGuid().ToString();
        Action result = () => _userService.RegisterUser(userLogin, password);
        result.Should().ThrowExactly<UserLoginAlreadyExistsException>();
    }

    [Test]
    public void LoginUser_ThrowsExceptionWhenUserLoginDoesNotExists()
    {
        var userDto = new LoginUserDto()
        {
            Login = Guid.NewGuid().ToString(),
            Password = Guid.NewGuid().ToString(),
        };
        Action result = () => _userService.LoginUser(userDto);
        result.Should().ThrowExactly<UserLoginDoesNotExistsException>();
    }

    [Test]
    public void LoginUser_ThrowsExceptionWhenPasswordsDontMatch()
    {
        var userDto = new LoginUserDto()
        {
            Login = "login1",
            Password = Guid.NewGuid().ToString(),
        };
        Action result = () => _userService.LoginUser(userDto);
        result.Should().ThrowExactly<UserPasswordDoesNotMachException>();
    }

    [Test]
    public void RefreshToken_ThrowsExceptionWhenUserDoesNotHaveValidRefreshToken()
    {
        var accessToken =
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyTG9naW4iOiIxMjM0IiwiZXhwIjoxNjcxMTE1NzY2LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MTYxLyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjcxNjEvIn0.1QWNjXgmmImta429fEZIIMAf4tXXPE_jbFhfbqXEFlI";
        var refreshToken = "DxfAmLujQwIj2zARTc5pNNLWFH8IRTyGz0zQAE9UoFM%3D";

        Action result = () => _userService.RefreshToken(refreshToken, accessToken);
        result.Should().ThrowExactly<UserDoesNotHaveValidRefreshTokenException>();
    }
}