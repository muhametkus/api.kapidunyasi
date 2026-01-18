namespace Api.KapiDunyasi.Application.Products.Dtos;

public class ProductImageUploadResponseDto
{
    public Guid ProductId { get; set; }
    public string ImageUrl { get; set; } = null!;
    public List<string> AllImages { get; set; } = new();
}
