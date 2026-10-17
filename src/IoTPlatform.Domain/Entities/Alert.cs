namespace IoTPlatform.Domain.Entities;

public class Alert
{
    public int Id { get; set; }
    public int? DeviceId { get; set; }
    public string Severity { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsAcknowledged { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Device? Device { get; set; }
}
