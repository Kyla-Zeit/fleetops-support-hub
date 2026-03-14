using FleetOpsSupportHub.Models;

namespace FleetOpsSupportHub.Data;

public static class SeedData
{
    public static void Initialize(AppDbContext db)
    {
        if (db.ClientAccounts.Any())
            return;

        var clients = new List<ClientAccount>
        {
            new()
            {
                Name = "NorthTrack Logistics",
                PrimaryContact = "Alicia Moore",
                Email = "alicia@northtrack.example",
                IsActive = true,
                CreatedUtc = DateTime.UtcNow.AddDays(-45)
            },
            new()
            {
                Name = "GeoRoute Transit",
                PrimaryContact = "Marc Pelletier",
                Email = "marc@georoute.example",
                IsActive = true,
                CreatedUtc = DateTime.UtcNow.AddDays(-30)
            }
        };

        db.ClientAccounts.AddRange(clients);
        db.SaveChanges();

        var tickets = new List<SupportTicket>
        {
            new()
            {
                Title = "Route optimization timeout during peak dispatch",
                Description = "The optimization engine times out for dispatch batches over 250 stops.",
                Status = "Escalated",
                Priority = "High",
                AssignedTo = "Rebecca Maguire",
                CreatedUtc = DateTime.UtcNow.AddDays(-8),
                UpdatedUtc = DateTime.UtcNow.AddDays(-1),
                ClientAccountId = clients[0].Id
            },
            new()
            {
                Title = "Driver tracking map pins stale after refresh",
                Description = "Vehicle positions lag by several minutes after browser refresh.",
                Status = "In Progress",
                Priority = "Medium",
                AssignedTo = "Rebecca Maguire",
                CreatedUtc = DateTime.UtcNow.AddDays(-5),
                UpdatedUtc = DateTime.UtcNow.AddHours(-10),
                ClientAccountId = clients[1].Id
            }
        };

        db.SupportTickets.AddRange(tickets);
        db.SaveChanges();

        var releases = new List<ReleaseItem>
        {
            new()
            {
                Version = "2026.3.1",
                Summary = "Hotfix for routing timeout and telemetry refresh handling.",
                ScheduledUtc = DateTime.UtcNow.AddDays(2).Date.AddHours(21),
                SundayNightRelease = true,
                Status = "Scheduled",
                SupportTicketId = tickets[0].Id
            },
            new()
            {
                Version = "2026.2.4",
                Summary = "Improved fleet dashboard filtering and ticket audit logging.",
                ScheduledUtc = DateTime.UtcNow.AddDays(-7).Date.AddHours(21),
                SundayNightRelease = true,
                Status = "Completed",
                SupportTicketId = tickets[1].Id
            }
        };

        db.ReleaseItems.AddRange(releases);
        db.SaveChanges();
    }
}
