using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cwiczenia11.Dtos.AuthDtos;
using Cwiczenia11.Entities;
using Cwiczenia11.Entities.AppUserEntities;
using Cwiczenia11.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Cwiczenia11.Services.AuthServices;

public class AuthService : IAuthService
{
    private readonly HospitalDbContext _context = new();

    private readonly IConfiguration _configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

    public async Task<int> Register(RegisterRequestDto registerRequestDto, CancellationToken cancellationToken)
    {
        if (await DoesUserExistBasedOnLogin(registerRequestDto.Login, cancellationToken))
        {
            throw new ArgumentException("User with the specified login already exist!");
        }

        var hashedPasswordAndSalt = SecurityHelper.GetHashedPasswordAndSalt(registerRequestDto.Password);

        var user = new AppUser
        {
            Login = registerRequestDto.Login,
            Password = hashedPasswordAndSalt.Item1,
            Salt = hashedPasswordAndSalt.Item2,
            RefreshToken = SecurityHelper.GenerateRefreshToken(),
            RefreshTokenExp = DateTime.Now.AddDays(1),
        };

        await _context
            .AppUsers
            .AddAsync(user, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return 1;
    }

    public async Task<TokensReponseDto> Login(LoginRequestDto loginRequestDto, CancellationToken cancellationToken)
    {
        if (await DoesUserExistBasedOnLogin(loginRequestDto.Login!, cancellationToken) == false)
        {
            throw new ArgumentException("User with the specified login does not exist!");
        }

        if (await IsTheProvidedPasswordRight(loginRequestDto, cancellationToken) == false)
        {
            throw new ArgumentException("Provided password is wrong!");
        }

        var user = await _context
            .AppUsers
            .Where(aa => aa.Login == loginRequestDto.Login)
            .FirstOrDefaultAsync(cancellationToken);

        var userClaim = new[]
        {
            new Claim(ClaimTypes.Name, loginRequestDto.Login!),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: userClaim,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: credentials
        );

        user!.RefreshToken = SecurityHelper.GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(1);

        await _context.SaveChangesAsync(cancellationToken);

        return new TokensReponseDto()
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            RefreshToken = user.RefreshToken
        };
    }

    public async Task<TokensReponseDto> RefreshAppUserToken(RefreshTokenRequestDto tokenRequestRequest,
        CancellationToken cancellationToken)
    {
        if (await DoesUserExistBasedOnRefreshToken(tokenRequestRequest.RefreshToken, cancellationToken) == false)
        {
            throw new ArgumentException("Incorrect refresh token!");
        }

        if (await HasRefreshTokenExpired(tokenRequestRequest.RefreshToken, cancellationToken))
        {
            throw new ArgumentException("Refresh token has already expired, login again!");
        }

        var user = await _context
            .AppUsers
            .Where(aa => aa.RefreshToken == tokenRequestRequest.RefreshToken)
            .FirstOrDefaultAsync(cancellationToken);

        var userClaim = new[]
        {
            new Claim(ClaimTypes.Name, user!.Login),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: userClaim,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: credentials
        );

        user.RefreshToken = SecurityHelper.GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(1);

        await _context.SaveChangesAsync(cancellationToken);

        return new TokensReponseDto()
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            RefreshToken = user.RefreshToken
        };
    }


    private async Task<bool> DoesUserExistBasedOnLogin(string login, CancellationToken cancellationToken)
    {
        var res = await _context
            .AppUsers
            .Where(aa => aa.Login == login)
            .FirstOrDefaultAsync(cancellationToken);

        return res != null;
    }

    private async Task<bool> IsTheProvidedPasswordRight(LoginRequestDto loginRequestDto,
        CancellationToken cancellationToken)
    {
        var user = await _context
            .AppUsers
            .Where(aa => aa.Login == loginRequestDto.Login)
            .FirstOrDefaultAsync(cancellationToken);

        var passwordFromDatabase = user!.Password;
        var requestHashedPasswordWithSalt =
            SecurityHelper.GetHashedPasswordWithSalt(loginRequestDto.Password!, user.Salt);

        return passwordFromDatabase == requestHashedPasswordWithSalt;
    }


    private async Task<bool> DoesUserExistBasedOnRefreshToken(string refreshToken, CancellationToken cancellationToken)
    {
        var res = await _context
            .AppUsers
            .Where(aa => aa.RefreshToken == refreshToken)
            .FirstOrDefaultAsync(cancellationToken);

        return res != null;
    }

    private async Task<bool> HasRefreshTokenExpired(string refreshToken, CancellationToken cancellationToken)
    {
        var res = await _context
            .AppUsers
            .Where(aa => aa.RefreshToken == refreshToken)
            .FirstOrDefaultAsync(cancellationToken);

        return res!.RefreshTokenExp < DateTime.Now;
    }
}