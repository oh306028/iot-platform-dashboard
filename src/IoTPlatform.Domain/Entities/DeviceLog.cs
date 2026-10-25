using IoTPlatform.Domain.Enums;

namespace IoTPlatform.Domain.Entities;

public class DeviceLog
{
    public long Id { get; set; }
    public int DeviceId { get; set; }
    public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;
    public DeviceProtocol Protocol { get; set; }
    public int? PayloadSize { get; set; }
    public string? IpAddress { get; set; }
    public bool IsSuccess { get; set; } = true;
    public string? ErrorMessage { get; set; }

    public Device Device { get; set; } = null!;
}

