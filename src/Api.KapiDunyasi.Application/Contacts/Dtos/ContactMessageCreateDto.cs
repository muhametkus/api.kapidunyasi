namespace Api.KapiDunyasi.Application.Contacts.Dtos;

public class ContactMessageCreateDto
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Message { get; set; } = null!;
}
