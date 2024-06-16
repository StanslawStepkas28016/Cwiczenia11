using Cwiczenia11.Dtos.AuthDtos;
using Cwiczenia11.Services.AuthServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenia11.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    /// <summary>
    ///     Endpoint used for registering a user, based on their login and password. A user cannot use an already taken login,
    ///     thus there is being validation preventing this from happening. 
    /// </summary>
    /// <param name="registerRequestDto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDto registerRequestDto,
        CancellationToken cancellationToken)
    {
        await _service.Register(registerRequestDto, cancellationToken);
        return Ok("Successfully registered!");
    }

    /// <summary>
    ///     Endpoint used for logging in a user of the application.
    ///     Input data is being validated (if the user exists and if its password is right).
    /// </summary>
    /// <param name="loginRequestDto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto loginRequestDto, CancellationToken cancellationToken)
    {
        var res = await _service.Login(loginRequestDto, cancellationToken);
        return Ok(res);
    }

    /// <summary>
    ///     Endpoint used for refreshing a user refreshToken. Based on the provided refreshToken.
    ///     This endpoint will prevent refreshing token if it has already expired or is non-existent.
    /// </summary>
    /// <param name="tokenRequestRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshAppUserToken(RefreshTokenRequestDto tokenRequestRequest,
        CancellationToken cancellationToken)
    {
        var res = await _service.RefreshAppUserToken(tokenRequestRequest, cancellationToken);
        return Ok(res);
    }
}