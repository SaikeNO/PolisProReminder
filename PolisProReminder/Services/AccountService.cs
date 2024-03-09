using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PolisProReminder.Entities;
using PolisProReminder.Exceptions;
using PolisProReminder.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PolisProReminder.Services;

public interface IAccountService
{
    public LoginResponseDto Login(LoginDto dto);
    public void ResetPassword(ResetPasswordDto dto);
    public TokenDto RefreshToken();
}
public class AccountService : IAccountService
{
    private readonly InsuranceDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly AuthenticationSettings _authenticationSettings;
    private readonly IUserContextService _userContextService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountService(InsuranceDbContext dbContext,
        IMapper mapper,
        IPasswordHasher<User> passwordHasher,
        AuthenticationSettings authenticationSettings,
        IUserContextService userContextService,
        IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        _authenticationSettings = authenticationSettings;
        _userContextService = userContextService;
        _httpContextAccessor = httpContextAccessor;

    }
    public LoginResponseDto Login(LoginDto dto)
    {
        var user = _dbContext.Users
            .Include(u => u.Role)
            .Include(u => u.RefreshToken)
            .FirstOrDefault(u => u.Name == dto.Name);

        if (user is null)
            throw new BadRequestException("Invalid name or password");

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

        if (result == PasswordVerificationResult.Failed)
            throw new BadRequestException("Invalid name or password");

        var refreshToken = GenerateRefreshToken();
        SetRefreshToken(refreshToken, user);

        return new LoginResponseDto()
        {
            Token = GenerateJwtToken(user),
            User = _mapper.Map<UserDto>(user)
        };

    }

    public TokenDto RefreshToken()
    {
        var user = _dbContext.Users
            .Include(u => u.Role)
            .Include(u => u.RefreshToken)
            .FirstOrDefault(u => u.Id == _userContextService.GetUserId);

        if (user is null)
            throw new NotFoundException("User does not exist");

        var refreshToken = _httpContextAccessor.HttpContext.Request.Cookies["refreshToken"];

        if (!user.RefreshToken.Token.Equals(refreshToken))
            throw new UnauthorizedException("Invalid Refresh Token");
        if (user.RefreshToken.Expires < DateTime.Now)
            throw new UnauthorizedException("Token expired");

        string token = GenerateJwtToken(user);
        var newRefreshToken = GenerateRefreshToken();
        SetRefreshToken(newRefreshToken, user);

        return new TokenDto() { Token = token };
    }

    public void ResetPassword(ResetPasswordDto dto)
    {
        var user = _dbContext.Users
            .FirstOrDefault(u => u.Id == _userContextService.GetUserId);

        if (user == null)
            throw new NotFoundException("User does not exist");

        user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);
        _dbContext.SaveChanges();
    }

    private RefreshToken GenerateRefreshToken()
    {
        var refreshToken = new RefreshToken()
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires = DateTime.Now.AddDays(_authenticationSettings.RefreshTokenExpireDays)
        };

        return refreshToken;
    }

    private void SetRefreshToken(RefreshToken newRefreshToken, User user)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = newRefreshToken.Expires,
        };

        _httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);


        if (user.RefreshToken is null)
        {
            user.RefreshToken = newRefreshToken;
        }
        else
        {
            user.RefreshToken.Token = newRefreshToken.Token;
            user.RefreshToken.Created = newRefreshToken.Created;
            user.RefreshToken.Expires = newRefreshToken.Expires;
        }

        _dbContext.SaveChanges();
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
            new Claim("SuperiorId", $"{user.SuperiorId}")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddHours(_authenticationSettings.JwtExpireHours);

        var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
            _authenticationSettings.JwtIssuer,
            claims,
            expires: expires,
            signingCredentials: cred);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
