using Api.KapiDunyasi.Application.Policies.Commands;
using Api.KapiDunyasi.Application.Policies.Dtos;
using Api.KapiDunyasi.Application.Policies.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.KapiDunyasi.WebAPI.Controllers;

[ApiController]
[Route("api/v1/policy")]
public class PolicyController : ControllerBase
{
    private readonly IMediator _mediator;

    public PolicyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<PolicyContentDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetPoliciesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{key}")]
    [ProducesResponseType(typeof(PolicyContentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByKey(string key, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetPolicyContentQuery(key), cancellationToken);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] PolicyCreateRequestDto payload, CancellationToken cancellationToken)
    {
        var key = await _mediator.Send(new CreatePolicyCommand(payload), cancellationToken);
        return StatusCode(StatusCodes.Status201Created, new { key });
    }

    [HttpPut("{key}")]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Update(string key, [FromBody] PolicyUpdateRequestDto payload, CancellationToken cancellationToken)
    {
        await _mediator.Send(new UpdatePolicyCommand(key, payload), cancellationToken);
        return NoContent();
    }

    [HttpDelete("{key}")]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(string key, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeletePolicyCommand(key), cancellationToken);
        return NoContent();
    }
}
