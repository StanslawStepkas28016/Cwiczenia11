using Cwiczenia11.ModelsDtos.AuthDtos;
using Cwiczenia11.Repositories.AuthRepositories;

namespace Cwiczenia11.Services.AuthServices;

public class AuthService : IAuthService
{
    private IAuthRepository _repository;

    public AuthService(IAuthRepository repository)
    {
        _repository = repository;
    }


    public async Task<int> Register(RegisterRequestDto registerRequestDto, CancellationToken cancellationToken)
    {
        var res = await _repository.Register(registerRequestDto, cancellationToken);

        return res;
    }

    public async Task<TokensReponseDto> Login(LoginRequestDto loginRequestDto, CancellationToken cancellationToken)
    {
        var res = await _repository.Login(loginRequestDto, cancellationToken);

        return res;
    }

    public async Task<TokensReponseDto> RefreshAppUserToken(RefreshTokenRequestDto tokenRequestRequest,
        CancellationToken cancellationToken)
    {
        var res = await _repository.RefreshAppUserToken(tokenRequestRequest, cancellationToken);

        return res;
    }
}