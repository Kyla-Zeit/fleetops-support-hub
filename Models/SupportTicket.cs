namespace FleetOpsSupportHub.Models;

public class SupportTicket
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = "Open";
    public string Priority { get; set; } = "Medium";
    public string AssignedTo { get; set; } = string.Empty;
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedUtc { get; set; } = DateTime.UtcNow;

    public int ClientAccountId { get; set; }
    public ClientAccount? ClientAccount { get; set; }

    public ICollection<ReleaseItem> ReleaseItems { get; set; } = new List<ReleaseItem>();
}
