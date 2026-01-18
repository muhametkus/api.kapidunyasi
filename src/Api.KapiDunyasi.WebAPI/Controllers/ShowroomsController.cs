using Api.KapiDunyasi.Application.Showrooms.Commands;
using Api.KapiDunyasi.Application.Showrooms.Dtos;
using Api.KapiDunyasi.Application.Showrooms.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.KapiDunyasi.WebAPI.Controllers;

[ApiController]
[Route("api/v1/showrooms")]
public class ShowroomsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ShowroomsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ShowroomDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetShowroomsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{slug}")]
    [ProducesResponseType(typeof(ShowroomDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBySlug(string slug, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetShowroomBySlugQuery(slug), cancellationToken);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpGet("id/{id:guid}")]
    [ProducesResponseType(typeof(ShowroomDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetShowroomByIdQuery(id), cancellationToken);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] ShowroomCreateRequestDto payload, CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(new CreateShowroomCommand(payload), cancellationToken);
        return StatusCode(StatusCodes.Status201Created, new { id });
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Update(Guid id, [FromBody] ShowroomUpdateRequestDto payload, CancellationToken cancellationToken)
    {
        await _mediator.Send(new UpdateShowroomCommand(id, payload), cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteShowroomCommand(id), cancellationToken);
        return NoContent();
    }
}
