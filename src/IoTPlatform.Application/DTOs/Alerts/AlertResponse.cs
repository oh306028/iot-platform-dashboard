using IoTPlatform.Domain.Enums;

namespace IoTPlatform.Application.DTOs.Alerts;

public class AlertResponse
{
    public int Id { get; set; }
    public int? DeviceId { get; set; }
    public string? DeviceName { get; set; }
    public AlertSeverity Severity { get; set; }
    public string SeverityName => Severity.GetDisplayName();
    public string Message { get; set; } = string.Empty;
    public bool IsAcknowledged { get; set; }
    public DateTime CreatedAt { get; set; }
}
