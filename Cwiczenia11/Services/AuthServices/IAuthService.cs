using Cwiczenia11.Dtos.AuthDtos;

namespace Cwiczenia11.Services.AuthServices;

public interface IAuthService
{
    public Task<int> Register(RegisterRequestDto registerRequestDto, CancellationToken cancellationToken);

    public Task<TokensReponseDto> Login(LoginRequestDto loginRequestDto, CancellationToken cancellationToken);

    public Task<TokensReponseDto> RefreshAppUserToken(RefreshTokenRequestDto tokenRequestRequest,
        CancellationToken cancellationToken);
}