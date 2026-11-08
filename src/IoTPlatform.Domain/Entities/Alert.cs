using IoTPlatform.Domain.Enums;

namespace IoTPlatform.Domain.Entities;

public class Alert
{
    public int Id { get; set; }
    public int? DeviceId { get; set; }
    public AlertSeverity Severity { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool IsAcknowledged { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Device? Device { get; set; }
}
