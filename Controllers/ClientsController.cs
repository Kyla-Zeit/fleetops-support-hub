using FleetOpsSupportHub.Data;
using FleetOpsSupportHub.Dtos;
using FleetOpsSupportHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FleetOpsSupportHub.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly AppDbContext _db;

    public ClientsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClientAccount>>> GetAll()
    {
        var clients = await _db.ClientAccounts
            .Include(c => c.Tickets)
            .OrderBy(c => c.Name)
            .ToListAsync();

        return Ok(clients);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClientAccount>> GetById(int id)
    {
        var client = await _db.ClientAccounts
            .Include(c => c.Tickets)
            .FirstOrDefaultAsync(c => c.Id == id);

        return client is null ? NotFound() : Ok(client);
    }

    [HttpPost]
    public async Task<ActionResult<ClientAccount>> Create(CreateClientAccountDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Email))
            return BadRequest("Name and Email are required.");

        var client = new ClientAccount
        {
            Name = dto.Name.Trim(),
            PrimaryContact = dto.PrimaryContact.Trim(),
            Email = dto.Email.Trim(),
            IsActive = dto.IsActive,
            CreatedUtc = DateTime.UtcNow
        };

        _db.ClientAccounts.Add(client);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = client.Id }, client);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ClientAccount>> Update(int id, UpdateClientAccountDto dto)
    {
        var client = await _db.ClientAccounts.FindAsync(id);
        if (client is null)
            return NotFound();

        client.Name = dto.Name.Trim();
        client.PrimaryContact = dto.PrimaryContact.Trim();
        client.Email = dto.Email.Trim();
        client.IsActive = dto.IsActive;

        await _db.SaveChangesAsync();
        return Ok(client);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var client = await _db.ClientAccounts.FindAsync(id);
        if (client is null)
            return NotFound();

        _db.ClientAccounts.Remove(client);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
