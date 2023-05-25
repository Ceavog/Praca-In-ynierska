using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Backend.DAL_EF;
using Backend.DataAccessLibrary;
using Backend.Repository;
using Backend.Repository.GenericRepositories;
using Backend.Repository.Interfaces;
using Backend.Services.Interface;
using Backend.Shared.Dtos;
using Backend.Shared.Dtos.UserDtos;
using Backend.Shared.Exceptions.UserExceptions;
using Mapster;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Services.Services;

public class UserService : IUserService
{
    private readonly IJwtConfiguration _jwtConfiguration;
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    public UserService(IJwtConfiguration jwtConfiguration, IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository)
    {
        _jwtConfiguration = jwtConfiguration;
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
    }
    /// <summary>
    /// this method adds User with default service point
    /// also login user and returns json web token
    /// </summary>
    /// <param name="login"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public void RegisterUser(string login, string password)
    {
        if (_userRepository.CheckIfLoginExists(login))
        {
            throw new UserLoginAlreadyExistsException(login);
        }   
        var user = new User()
        {
            Login = login,
            Password =  BCrypt.Net.BCrypt.HashPassword(password),
        };
        
        var addedUser = _userRepository.Add(user);

        var refreshToken = GenerateRefreshToken(addedUser.Id);
        _refreshTokenRepository.Add(refreshToken);
    }

    
    /// <summary>
    /// authenticate user and returns json web token if authentication pass
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public AuthenticatedUserResposeDto LoginUser(LoginUserDto user)
    {
        if (!_userRepository.CheckIfLoginExists(user.Login))
        {
            throw new UserLoginDoesNotExistsException(user.Login);
        }
        var authenticatedUser = _userRepository.GetUserByLogin(user.Login);
        

        if (!BCrypt.Net.BCrypt.Verify(user.Password, authenticatedUser.Password))
        {
            throw new UserPasswordDoesNotMachException();

        }

        var refreshToken = new RefreshToken();
        if (_refreshTokenRepository.CheckIfUserHasValidRefreshToken(authenticatedUser.Id))
        {
            refreshToken = GenerateRefreshToken(authenticatedUser.Id);
            _refreshTokenRepository.Add(refreshToken);
        }

        var accessToken = GenerateAccessToken(user);
        return new AuthenticatedUserResposeDto { RefreshToken = refreshToken.Token, AccessToken = accessToken };
    }

    public AuthenticatedUserResposeDto RefreshToken(string refreshToken, string accessToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.ReadJwtToken(accessToken);
        var userLogin = token.Claims.FirstOrDefault(x => x.Type.Equals("userLogin"))!.Value;
        
        var user = _userRepository.GetUserByLogin(userLogin);
        if (_refreshTokenRepository.CheckIfUserHasValidRefreshToken(user.Id, refreshToken))
        {
            return new AuthenticatedUserResposeDto
            {
                RefreshToken = refreshToken,
                AccessToken = GenerateAccessToken(user.Adapt<LoginUserDto>())
            };
        }
        else
        {
            throw new UserDoesNotHaveValidRefreshTokenException(user.Id);
        }
    }

    public UserDto GetAllDataAboutUser(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var parsedToken = handler.ReadJwtToken(token);
        var userLogin = parsedToken.Claims.FirstOrDefault(x => x.Type.Equals("userLogin")).Value;
        return _userRepository.GetUserByLogin(userLogin).Adapt<UserDto>();
    }


    #region Private functions
   
    private string GenerateAccessToken(LoginUserDto userDto)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //here i can add any claims
        var claims = new[] {    
            new Claim("userLogin", userDto.Login),
        };


        var issuer = _jwtConfiguration.Issuer;
        var token = new JwtSecurityToken(
            issuer,
            issuer,
            claims,
            expires: DateTime.Now.AddMinutes(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private RefreshToken GenerateRefreshToken(int addedUserId)
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        
        var refreshToken = new RefreshToken
        {
            Token = Convert.ToBase64String(randomNumber),
            ExpirationTime = DateTime.Now.AddMonths(1),
            UserId = addedUserId
        };
        return refreshToken;
    }
    #endregion

}