using Api.KapiDunyasi.Domain.Common;

namespace Api.KapiDunyasi.Domain.Faqs;

public class Faq : AggregateRoot
{
    public string QTR { get; private set; }
    public string ATR { get; private set; }
    public int SortOrder { get; private set; }

    protected Faq() { }

    public Faq(string qTR, string aTR, int sortOrder = 0)
    {
        QTR = qTR;
        ATR = aTR;
        SortOrder = sortOrder;
    }
}