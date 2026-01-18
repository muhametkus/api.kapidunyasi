using Api.KapiDunyasi.Application.Products.Dtos;
using Api.KapiDunyasi.Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.KapiDunyasi.WebAPI.Controllers;

[ApiController]
[Route("api/v1/featured")]
public class FeaturedController : ControllerBase
{
    private readonly IMediator _mediator;

    public FeaturedController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ProductListDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetFeaturedProductsQuery(), cancellationToken);
        return Ok(result);
    }
}
