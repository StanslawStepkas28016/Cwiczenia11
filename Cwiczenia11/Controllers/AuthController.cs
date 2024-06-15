using Cwiczenia11.ModelsDtos.AuthDtos;
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

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDto registerRequestDto,
        CancellationToken cancellationToken)
    {
        try
        {
            var res = await _service.Register(registerRequestDto, cancellationToken);

            if (res == -1)
            {
                return Conflict("User with the specified login already exist!");
            }

            return Ok("Registered!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto loginRequestDto, CancellationToken cancellationToken)
    {
        try
        {
            var res = await _service.Login(loginRequestDto, cancellationToken);

            if (res.StatusCode == -1)
            {
                return BadRequest("User with the specified login does not exist!");
            }

            if (res.StatusCode == -2)
            {
                return Unauthorized("Provided password is wrong!");
            }

            return Ok(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [Authorize]
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshAppUserToken(RefreshTokenRequestDto tokenRequestRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            var res = await _service.RefreshAppUserToken(tokenRequestRequest, cancellationToken);

            if (res.StatusCode == -1)
            {
                return NotFound("Incorrect refresh token!");
            }

            if (res.StatusCode == -2)
            {
                return NotFound("Refresh token has already expired, login again!");
            }

            return Ok(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}