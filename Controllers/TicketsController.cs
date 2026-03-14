using FleetOpsSupportHub.Data;
using FleetOpsSupportHub.Dtos;
using FleetOpsSupportHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FleetOpsSupportHub.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private static readonly HashSet<string> ValidStatuses = new(StringComparer.OrdinalIgnoreCase)
    {
        "Open", "In Progress", "Escalated", "Resolved", "Closed"
    };

    private static readonly HashSet<string> ValidPriorities = new(StringComparer.OrdinalIgnoreCase)
    {
        "Low", "Medium", "High", "Critical"
    };

    private readonly AppDbContext _db;

    public TicketsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SupportTicket>>> GetAll([FromQuery] string? status, [FromQuery] string? priority)
    {
        var query = _db.SupportTickets
            .Include(t => t.ClientAccount)
            .Include(t => t.ReleaseItems)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(status))
            query = query.Where(t => t.Status == status);

        if (!string.IsNullOrWhiteSpace(priority))
            query = query.Where(t => t.Priority == priority);

        var tickets = await query
            .OrderByDescending(t => t.UpdatedUtc)
            .ToListAsync();

        return Ok(tickets);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<SupportTicket>> GetById(int id)
    {
        var ticket = await _db.SupportTickets
            .Include(t => t.ClientAccount)
            .Include(t => t.ReleaseItems)
            .FirstOrDefaultAsync(t => t.Id == id);

        return ticket is null ? NotFound() : Ok(ticket);
    }

    [HttpPost]
    public async Task<ActionResult<SupportTicket>> Create(CreateSupportTicketDto dto)
    {
        if (!ValidStatuses.Contains(dto.Status) || !ValidPriorities.Contains(dto.Priority))
            return BadRequest("Invalid status or priority.");

        var clientExists = await _db.ClientAccounts.AnyAsync(c => c.Id == dto.ClientAccountId);
        if (!clientExists)
            return BadRequest("ClientAccountId does not exist.");

        var ticket = new SupportTicket
        {
            Title = dto.Title.Trim(),
            Description = dto.Description.Trim(),
            Status = dto.Status.Trim(),
            Priority = dto.Priority.Trim(),
            AssignedTo = dto.AssignedTo.Trim(),
            ClientAccountId = dto.ClientAccountId,
            CreatedUtc = DateTime.UtcNow,
            UpdatedUtc = DateTime.UtcNow
        };

        _db.SupportTickets.Add(ticket);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = ticket.Id }, ticket);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<SupportTicket>> Update(int id, UpdateSupportTicketDto dto)
    {
        var ticket = await _db.SupportTickets.FindAsync(id);
        if (ticket is null)
            return NotFound();

        if (!ValidStatuses.Contains(dto.Status) || !ValidPriorities.Contains(dto.Priority))
            return BadRequest("Invalid status or priority.");

        ticket.Title = dto.Title.Trim();
        ticket.Description = dto.Description.Trim();
        ticket.Status = dto.Status.Trim();
        ticket.Priority = dto.Priority.Trim();
        ticket.AssignedTo = dto.AssignedTo.Trim();
        ticket.ClientAccountId = dto.ClientAccountId;
        ticket.UpdatedUtc = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return Ok(ticket);
    }

    [HttpPatch("{id:int}/resolve")]
    public async Task<ActionResult<SupportTicket>> Resolve(int id)
    {
        var ticket = await _db.SupportTickets.FindAsync(id);
        if (ticket is null)
            return NotFound();

        ticket.Status = "Resolved";
        ticket.UpdatedUtc = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return Ok(ticket);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ticket = await _db.SupportTickets.FindAsync(id);
        if (ticket is null)
            return NotFound();

        _db.SupportTickets.Remove(ticket);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
