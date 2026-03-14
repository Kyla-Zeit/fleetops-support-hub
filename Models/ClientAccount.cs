namespace FleetOpsSupportHub.Models;

public class ClientAccount
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PrimaryContact { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

    public ICollection<SupportTicket> Tickets { get; set; } = new List<SupportTicket>();
}
