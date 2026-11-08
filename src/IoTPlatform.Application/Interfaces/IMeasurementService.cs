using IoTPlatform.Application.DTOs.Measurements;
using IoTPlatform.Domain.Enums;

namespace IoTPlatform.Application.Interfaces;

public interface IMeasurementService
{
    Task<IEnumerable<MeasurementResponse>> GetByDeviceIdAsync(
        int deviceId,
        MetricType? metricType = null,
        DateTime? from = null,
        DateTime? to = null,
        int limit = 100);

    Task<IEnumerable<MeasurementResponse>> GetLatestByDeviceIdAsync(int deviceId);

    Task<MeasurementResponse> CreateAsync(int deviceId, CreateMeasurementRequest request);
}
