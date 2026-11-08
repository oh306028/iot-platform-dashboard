using IoTPlatform.Domain.Enums;

namespace IoTPlatform.Domain.Entities;

public class Measurement
{
    public long Id { get; set; }
    public int DeviceId { get; set; }
    public MetricType MetricType { get; set; }
    public double Value { get; set; }
    public string Unit { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public Device Device { get; set; } = null!;
}
