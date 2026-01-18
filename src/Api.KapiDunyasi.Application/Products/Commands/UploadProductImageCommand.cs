using MediatR;

namespace Api.KapiDunyasi.Application.Products.Commands;

public record UploadProductImageCommand(
    Guid ProductId, 
    Stream FileStream, 
    string FileName
) : IRequest<string>;
