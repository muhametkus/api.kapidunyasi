namespace Api.KapiDunyasi.Application.Contacts.Dtos;

public class ContactMessageResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Message { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
