using Api.KapiDunyasi.Application.Auth.Commands;
using Api.KapiDunyasi.Application.Auth.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.KapiDunyasi.WebAPI.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto payload, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RegisterCommand(payload), cancellationToken);
        return Ok(result);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto payload, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new LoginCommand(payload), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Token'ın geçerli olup olmadığını test et ve claim'leri döndür
    /// </summary>
    [HttpGet("test")]
    [Authorize]
    public IActionResult TestAuth()
    {
        var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
        return Ok(new
        {
            IsAuthenticated = User.Identity?.IsAuthenticated,
            Name = User.Identity?.Name,
            Claims = claims
        });
    }

    /// <summary>
    /// Admin yetkisini test et
    /// </summary>
    [HttpGet("test-admin")]
    [Authorize(Policy = "AdminOnly")]
    public IActionResult TestAdminAuth()
    {
        var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
        return Ok(new
        {
            IsAuthenticated = User.Identity?.IsAuthenticated,
            IsAdmin = true,
            Name = User.Identity?.Name,
            Claims = claims
        });
    }
}
