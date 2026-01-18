using Api.KapiDunyasi.Application.Users.Commands;
using Api.KapiDunyasi.Application.Users.Dtos;
using Api.KapiDunyasi.Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.KapiDunyasi.WebAPI.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Tüm kullanıcıları listele (Sadece Admin)
    /// </summary>
    [HttpGet]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(typeof(List<UserResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetUsersQuery(), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// ID ile kullanıcı getir (Admin veya kendi profili)
    /// </summary>
    [HttpGet("{id:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var currentUserId = GetCurrentUserId();
        var isAdmin = IsAdmin();

        // Admin değilse ve kendi profili değilse erişim engelle
        if (!isAdmin && currentUserId != id)
        {
            return Forbid();
        }

        var result = await _mediator.Send(new GetUserByIdQuery(id), cancellationToken);
        return result == null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Kendi profilimi getir
    /// </summary>
    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMe(CancellationToken cancellationToken)
    {
        var currentUserId = GetCurrentUserId();
        var result = await _mediator.Send(new GetUserByIdQuery(currentUserId), cancellationToken);
        return result == null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Yeni kullanıcı oluştur (Sadece Admin)
    /// </summary>
    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] UserCreateRequestDto payload, CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(new CreateUserCommand(payload), cancellationToken);
        return StatusCode(StatusCodes.Status201Created, new { id });
    }

    /// <summary>
    /// Kendi profilimi güncelle
    /// </summary>
    [HttpPut("me")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateMe([FromBody] UserUpdateRequestDto payload, CancellationToken cancellationToken)
    {
        var currentUserId = GetCurrentUserId();
        await _mediator.Send(new UpdateUserCommand(currentUserId, payload, currentUserId), cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Kullanıcı güncelle (Admin veya kendi profili)
    /// </summary>
    [HttpPut("{id:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UserUpdateRequestDto payload, CancellationToken cancellationToken)
    {
        var currentUserId = GetCurrentUserId();
        var isAdmin = IsAdmin();

        // Admin ise AdminUpdateUserCommand kullan (rol değiştirebilir)
        if (isAdmin)
        {
            var adminPayload = new AdminUserUpdateRequestDto
            {
                Name = payload.Name,
                Email = payload.Email,
                Password = payload.Password
            };
            await _mediator.Send(new AdminUpdateUserCommand(id, adminPayload), cancellationToken);
            return NoContent();
        }

        // Admin değilse sadece kendi profilini güncelleyebilir
        if (currentUserId != id)
        {
            return Forbid();
        }

        await _mediator.Send(new UpdateUserCommand(id, payload, currentUserId), cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Kullanıcı güncelle - Admin için (rol dahil)
    /// </summary>
    [HttpPut("{id:guid}/admin")]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> AdminUpdate(Guid id, [FromBody] AdminUserUpdateRequestDto payload, CancellationToken cancellationToken)
    {
        await _mediator.Send(new AdminUpdateUserCommand(id, payload), cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Kullanıcı sil (Sadece Admin)
    /// </summary>
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteUserCommand(id), cancellationToken);
        return NoContent();
    }

    private Guid GetCurrentUserId()
    {
        // MapInboundClaims = false olduğunda claim'ler orijinal isimleriyle gelir
        var userIdClaim = User.FindFirst("sub")?.Value 
                          ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                          ?? User.FindFirst("id")?.Value;
        
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
        {
            throw new UnauthorizedAccessException("Kullanici kimlik dogrulamasi basarisiz.");
        }

        return userId;
    }

    private bool IsAdmin()
    {
        // ClaimTypes.Role = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
        var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value 
                        ?? User.FindFirst("role")?.Value;
        
        return string.Equals(roleClaim, "Admin", StringComparison.OrdinalIgnoreCase);
    }
}
