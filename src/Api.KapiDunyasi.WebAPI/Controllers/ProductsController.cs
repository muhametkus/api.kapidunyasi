using Api.KapiDunyasi.Application.Common.Models;
using Api.KapiDunyasi.Application.Products.Commands;
using Api.KapiDunyasi.Application.Products.Dtos;
using Api.KapiDunyasi.Application.Products.Queries;
using Api.KapiDunyasi.Application.Products.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.KapiDunyasi.WebAPI.Controllers;

[ApiController]
[Route("api/v1/products")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<ProductListDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? category,
        [FromQuery] string? series,
        [FromQuery] string? search,
        [FromQuery] string? sort,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 12,
        [FromQuery(Name = "filters[fireResistant]")] bool? fireResistant = null,
        [FromQuery(Name = "filters[stockStatus]")] string? stockStatus = null,
        [FromQuery(Name = "filters[surfaceTR]")] string? surfaceTR = null,
        [FromQuery(Name = "filters[colorTR]")] string? colorTR = null,
        [FromQuery(Name = "filters[priceRange]")] string? priceRange = null,
        CancellationToken cancellationToken = default)
    {
        decimal? priceMin = null;
        decimal? priceMax = null;
        if (!string.IsNullOrWhiteSpace(priceRange))
        {
            var parts = priceRange.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (parts.Length > 0 && decimal.TryParse(parts[0], out var parsedMin))
            {
                priceMin = parsedMin;
            }
            if (parts.Length > 1 && decimal.TryParse(parts[1], out var parsedMax))
            {
                priceMax = parsedMax;
            }
        }

        var request = new ProductListRequest
        {
            Category = category,
            Series = series,
            Search = search,
            Sort = sort,
            Page = page,
            PageSize = pageSize,
            FireResistant = fireResistant,
            StockStatus = stockStatus,
            SurfaceTR = surfaceTR,
            ColorTR = colorTR,
            PriceMin = priceMin,
            PriceMax = priceMax
        };

        var result = await _mediator.Send(new GetProductsQuery(request), cancellationToken);
        return Ok(result);
    }

    [HttpGet("related")]
    [ProducesResponseType(typeof(List<ProductListDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRelated(
        [FromQuery] Guid? categoryId,
        [FromQuery] string? series,
        [FromQuery] Guid? excludeId,
        [FromQuery] int take = 8,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetRelatedProductsQuery(categoryId, series, excludeId, take), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetProductByIdQuery(id), cancellationToken);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] ProductCreateRequestDto payload, CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(new CreateProductCommand(payload), cancellationToken);
        return StatusCode(StatusCodes.Status201Created, new { id });
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Update(Guid id, [FromBody] ProductUpdateRequestDto payload, CancellationToken cancellationToken)
    {
        await _mediator.Send(new UpdateProductCommand(id, payload), cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteProductCommand(id), cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Ürüne fotoğraf yükle
    /// </summary>
    [HttpPost("{id:guid}/images")]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadImage(Guid id, IFormFile file, CancellationToken cancellationToken)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest(new { error = "Dosya yüklenmedi." });
        }

        // Dosya boyutu kontrolü (max 10MB)
        if (file.Length > 10 * 1024 * 1024)
        {
            return BadRequest(new { error = "Dosya boyutu 10MB'dan büyük olamaz." });
        }

        await using var stream = file.OpenReadStream();
        var imageUrl = await _mediator.Send(
            new UploadProductImageCommand(id, stream, file.FileName), 
            cancellationToken);

        return StatusCode(StatusCodes.Status201Created, new { imageUrl });
    }

    /// <summary>
    /// Birden fazla fotoğraf yükle
    /// </summary>
    [HttpPost("{id:guid}/images/batch")]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadImages(Guid id, List<IFormFile> files, CancellationToken cancellationToken)
    {
        if (files == null || files.Count == 0)
        {
            return BadRequest(new { error = "Dosya yüklenmedi." });
        }

        var uploadedUrls = new List<string>();
        
        foreach (var file in files)
        {
            if (file.Length == 0) continue;
            if (file.Length > 10 * 1024 * 1024) continue; // Skip files > 10MB

            await using var stream = file.OpenReadStream();
            var imageUrl = await _mediator.Send(
                new UploadProductImageCommand(id, stream, file.FileName), 
                cancellationToken);
            uploadedUrls.Add(imageUrl);
        }

        return StatusCode(StatusCodes.Status201Created, new { images = uploadedUrls, count = uploadedUrls.Count });
    }

    /// <summary>
    /// Üründen fotoğraf sil
    /// </summary>
    [HttpDelete("{id:guid}/images")]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteImage(Guid id, [FromQuery] string imageUrl, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
        {
            return BadRequest(new { error = "imageUrl gerekli." });
        }

        await _mediator.Send(new DeleteProductImageCommand(id, imageUrl), cancellationToken);
        return NoContent();
    }
}

