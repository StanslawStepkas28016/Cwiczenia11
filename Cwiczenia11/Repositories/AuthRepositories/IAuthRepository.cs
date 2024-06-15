using Cwiczenia11.ModelsDtos.AuthDtos;

namespace Cwiczenia11.Repositories.AuthRepositories;

public interface IAuthRepository
{
    public Task<int> Register(RegisterRequestDto registerRequestDto, CancellationToken cancellationToken);

    public Task<TokensReponseDto> Login(LoginRequestDto loginRequestDto, CancellationToken cancellationToken);

    public Task<TokensReponseDto> RefreshAppUserToken(RefreshTokenRequestDto tokenRequestRequest,
        CancellationToken cancellationToken);
}