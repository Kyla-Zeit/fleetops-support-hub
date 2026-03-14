namespace FleetOpsSupportHub.Models;

public class ReleaseItem
{
    public int Id { get; set; }
    public string Version { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public DateTime ScheduledUtc { get; set; }
    public bool SundayNightRelease { get; set; }
    public string Status { get; set; } = "Scheduled";

    public int? SupportTicketId { get; set; }
    public SupportTicket? SupportTicket { get; set; }
}
