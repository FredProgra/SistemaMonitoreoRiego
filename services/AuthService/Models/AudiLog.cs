class AudiLog
{
     public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public string Action { get; set; } = string.Empty;
    public string Resource { get; set; } = string.Empty;

    public string? IpAddress { get; set; }
    public string? Details { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public User? User { get; set; }
}