namespace Api.KapiDunyasi.Domain.Policies;

public class PolicyContent
{
    public string Key { get; private set; } // gizlilik | iptal | kvkk | mesafeli
    public string TitleTR { get; private set; }
    public string BodyTR { get; private set; }

    protected PolicyContent() { }

    public PolicyContent(string key, string titleTR, string bodyTR)
    {
        Key = key;
        TitleTR = titleTR;
        BodyTR = bodyTR;
    }

    public void UpdateContent(string titleTR, string bodyTR)
    {
        TitleTR = titleTR;
        BodyTR = bodyTR;
    }
}