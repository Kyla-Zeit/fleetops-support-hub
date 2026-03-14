namespace FleetOpsSupportHub.Dtos;

public class CreateSupportTicketDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = "Open";
    public string Priority { get; set; } = "Medium";
    public string AssignedTo { get; set; } = string.Empty;
    public int ClientAccountId { get; set; }
}

public class UpdateSupportTicketDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = "Open";
    public string Priority { get; set; } = "Medium";
    public string AssignedTo { get; set; } = string.Empty;
    public int ClientAccountId { get; set; }
}
