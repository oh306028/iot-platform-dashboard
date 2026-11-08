using IoTPlatform.Domain.Enums;

namespace IoTPlatform.Application.DTOs.Measurements;

public class CreateMeasurementRequest
{
    public MetricType MetricType { get; set; }
    public double Value { get; set; }
    public string Unit { get; set; } = string.Empty;
    public DateTime? Timestamp { get; set; }
}
