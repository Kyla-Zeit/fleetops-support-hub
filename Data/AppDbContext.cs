using FleetOpsSupportHub.Models;
using Microsoft.EntityFrameworkCore;

namespace FleetOpsSupportHub.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ClientAccount> ClientAccounts => Set<ClientAccount>();
    public DbSet<SupportTicket> SupportTickets => Set<SupportTicket>();
    public DbSet<ReleaseItem> ReleaseItems => Set<ReleaseItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClientAccount>()
            .HasMany(c => c.Tickets)
            .WithOne(t => t.ClientAccount!)
            .HasForeignKey(t => t.ClientAccountId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SupportTicket>()
            .HasMany(t => t.ReleaseItems)
            .WithOne(r => r.SupportTicket!)
            .HasForeignKey(r => r.SupportTicketId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<ClientAccount>()
            .Property(c => c.Name)
            .HasMaxLength(120);

        modelBuilder.Entity<SupportTicket>()
            .Property(t => t.Title)
            .HasMaxLength(160);

        modelBuilder.Entity<ReleaseItem>()
            .Property(r => r.Version)
            .HasMaxLength(40);
    }
}
