namespace Ms06ServicesContracts.Models;

public class ServiceAppointment
{
    public Guid Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ServiceType { get; set; } = string.Empty;
    public DateTime ScheduledAt { get; set; }
    public string Status { get; set; } = "PENDING";
    public string? Notes { get; set; }
    public Guid CreatedByUserId { get; set; }
    public DateTime CreatedAt { get; set; }

    public bool EstaPendiente() => Status == "PENDING";
}