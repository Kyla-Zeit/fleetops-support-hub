using FleetOpsSupportHub.Data;
using FleetOpsSupportHub.Dtos;
using FleetOpsSupportHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FleetOpsSupportHub.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReleasesController : ControllerBase
{
    private readonly AppDbContext _db;

    public ReleasesController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReleaseItem>>> GetAll([FromQuery] bool? sundayNightOnly)
    {
        var query = _db.ReleaseItems
            .Include(r => r.SupportTicket)
            .ThenInclude(t => t!.ClientAccount)
            .AsQueryable();

        if (sundayNightOnly == true)
            query = query.Where(r => r.SundayNightRelease);

        var releases = await query
            .OrderByDescending(r => r.ScheduledUtc)
            .ToListAsync();

        return Ok(releases);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ReleaseItem>> GetById(int id)
    {
        var release = await _db.ReleaseItems
            .Include(r => r.SupportTicket)
            .ThenInclude(t => t!.ClientAccount)
            .FirstOrDefaultAsync(r => r.Id == id);

        return release is null ? NotFound() : Ok(release);
    }

    [HttpPost]
    public async Task<ActionResult<ReleaseItem>> Create(CreateReleaseItemDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Version))
            return BadRequest("Version is required.");

        if (dto.SupportTicketId.HasValue)
        {
            var exists = await _db.SupportTickets.AnyAsync(t => t.Id == dto.SupportTicketId.Value);
            if (!exists)
                return BadRequest("SupportTicketId does not exist.");
        }

        var release = new ReleaseItem
        {
            Version = dto.Version.Trim(),
            Summary = dto.Summary.Trim(),
            ScheduledUtc = dto.ScheduledUtc,
            SundayNightRelease = dto.SundayNightRelease,
            Status = dto.Status.Trim(),
            SupportTicketId = dto.SupportTicketId
        };

        _db.ReleaseItems.Add(release);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = release.Id }, release);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ReleaseItem>> Update(int id, UpdateReleaseItemDto dto)
    {
        var release = await _db.ReleaseItems.FindAsync(id);
        if (release is null)
            return NotFound();

        release.Version = dto.Version.Trim();
        release.Summary = dto.Summary.Trim();
        release.ScheduledUtc = dto.ScheduledUtc;
        release.SundayNightRelease = dto.SundayNightRelease;
        release.Status = dto.Status.Trim();
        release.SupportTicketId = dto.SupportTicketId;

        await _db.SaveChangesAsync();
        return Ok(release);
    }

    [HttpPatch("{id:int}/complete")]
    public async Task<ActionResult<ReleaseItem>> Complete(int id)
    {
        var release = await _db.ReleaseItems.FindAsync(id);
        if (release is null)
            return NotFound();

        release.Status = "Completed";
        await _db.SaveChangesAsync();

        return Ok(release);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var release = await _db.ReleaseItems.FindAsync(id);
        if (release is null)
            return NotFound();

        _db.ReleaseItems.Remove(release);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
