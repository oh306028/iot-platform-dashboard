using IoTPlatform.Domain.Enums;

namespace IoTPlatform.Application.DTOs.Alerts;

public class CreateAlertRequest
{
    public int? DeviceId { get; set; }
    public AlertSeverity Severity { get; set; }
    public string Message { get; set; } = string.Empty;
}
