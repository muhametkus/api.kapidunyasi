using Api.KapiDunyasi.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.WebAPI.Controllers;

[ApiController]
[Route("api/health")]
public class HealthController : ControllerBase
{
    private readonly AppDbContext _db;

    public HealthController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("db")]
    public async Task<IActionResult> CheckDb()
    {
        var canConnect = await _db.Database.CanConnectAsync();
        return Ok(new
        {
            database = canConnect ? "UP" : "DOWN"
        });
    }
}