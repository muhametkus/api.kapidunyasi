using Api.KapiDunyasi.Domain.Common;

namespace Api.KapiDunyasi.Domain.Contacts;

public class ContactMessage : AggregateRoot
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Message { get; private set; }

    protected ContactMessage() { }

    public ContactMessage(string name, string email, string message)
    {
        Name = name;
        Email = email;
        Message = message;
    }
}