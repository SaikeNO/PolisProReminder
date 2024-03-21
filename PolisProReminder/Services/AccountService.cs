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
    Task<LoginResponseDto> Login(LoginDto dto);
    Task<TokenDto> RefreshToken(string expiredToken);
    Task ResetPassword(ResetPasswordDto dto);
}

public class AccountService(InsuranceDbContext dbContext,
    IMapper mapper,
    IPasswordHasher<User> passwordHasher,
    AuthenticationSettings authenticationSettings,
    IUserContextService userContextService,
    IHttpContextAccessor httpContextAccessor) : IAccountService
{
    public async Task<LoginResponseDto> Login(LoginDto dto)
    {
        var user = await dbContext.Users
            .Include(u => u.Role)
            .Include(u => u.RefreshToken)
            .FirstOrDefaultAsync(u => u.Name == dto.Name);

        if (user is null)
            throw new BadRequestException("Invalid name or password");

        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

        if (result == PasswordVerificationResult.Failed)
            throw new BadRequestException("Invalid name or password");

        var refreshToken = GenerateRefreshToken();
        await SetRefreshToken(refreshToken, user);

        return new LoginResponseDto()
        {
            Token = GenerateJwtToken(user),
            User = mapper.Map<UserDto>(user)
        };

    }

    public async Task<TokenDto> RefreshToken(string expiredToken)
    {
        var principal = GetPrincipalFromExpiredToken(expiredToken);
        var userId = principal?.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (userId is null)
            throw new UnauthorizedException("Invalid Access Token");

        var user = await dbContext.Users
            .Include(u => u.Role)
            .Include(u => u.RefreshToken)
            .FirstOrDefaultAsync(u => u.Id == int.Parse(userId));

        if (user is null)
            throw new NotFoundException("User does not exist");

        var refreshToken = httpContextAccessor?.HttpContext?.Request.Cookies["refreshToken"];

        if (user.RefreshToken is null || !user.RefreshToken.Token.Equals(refreshToken))
            throw new UnauthorizedException("Invalid Refresh Token");
        if (user.RefreshToken.Expires < DateTime.Now)
            throw new UnauthorizedException("Token expired");

        string token = GenerateJwtToken(user);
        var newRefreshToken = GenerateRefreshToken();
        await SetRefreshToken(newRefreshToken, user);

        return new TokenDto() { Token = token };
    }

    public async Task ResetPassword(ResetPasswordDto dto)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == userContextService.GetUserId);

        if (user == null)
            throw new NotFoundException("User does not exist");

        user.PasswordHash = passwordHasher.HashPassword(user, dto.Password);
        await dbContext.SaveChangesAsync();
    }

    private async Task SetRefreshToken(RefreshToken newRefreshToken, User user)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = newRefreshToken.Expires,
            IsEssential = true,
            Secure = true,
            SameSite = SameSiteMode.None,
        };

        httpContextAccessor.HttpContext?.Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);


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

        await dbContext.SaveChangesAsync();
    }

    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var validation = new TokenValidationParameters
        {
            ValidIssuer = authenticationSettings.JwtIssuer,
            ValidAudience = authenticationSettings.JwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
            ValidateLifetime = false
        };

        return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
    }
    private RefreshToken GenerateRefreshToken()
    {
        var refreshToken = new RefreshToken()
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires = DateTime.Now.AddDays(authenticationSettings.RefreshTokenExpireDays)
        };

        return refreshToken;
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new(ClaimTypes.Role, $"{user.Role.Name}"),
            new("SuperiorId", $"{user.SuperiorId}")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddHours(authenticationSettings.JwtExpireHours);

        var token = new JwtSecurityToken(authenticationSettings.JwtIssuer,
            authenticationSettings.JwtIssuer,
            claims,
            expires: expires,
            signingCredentials: cred);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
