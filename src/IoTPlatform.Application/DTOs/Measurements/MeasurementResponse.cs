using IoTPlatform.Domain.Enums;

namespace IoTPlatform.Application.DTOs.Measurements;

public class MeasurementResponse
{
    public long Id { get; set; }
    public int DeviceId { get; set; }
    public MetricType MetricType { get; set; }
    public string MetricTypeName => MetricType.GetDisplayName();
    public double Value { get; set; }
    public string Unit { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}
