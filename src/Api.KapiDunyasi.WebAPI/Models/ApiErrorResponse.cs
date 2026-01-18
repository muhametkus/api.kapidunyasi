namespace Api.KapiDunyasi.WebAPI.Models;

public class ApiErrorResponse
{
    public string Type { get; set; } = null!;
    public string Title { get; set; } = null!;
    public int Status { get; set; }
    public string TraceId { get; set; } = null!;
    public Dictionary<string, string[]> Errors { get; set; } = new();
}
