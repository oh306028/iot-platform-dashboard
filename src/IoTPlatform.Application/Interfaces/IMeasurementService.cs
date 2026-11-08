using IoTPlatform.Application.DTOs.Common;
using IoTPlatform.Application.DTOs.Measurements;
using IoTPlatform.Domain.Enums;

namespace IoTPlatform.Application.Interfaces;

public interface IMeasurementService
{
    Task<PagedResult<MeasurementResponse>> GetByDeviceIdAsync(
        int deviceId,
        MetricType? metricType = null,
        DateTime? from = null,
        DateTime? to = null,
        int pageNumber = 1,
        int pageSize = 10);

    Task<IEnumerable<MeasurementResponse>> GetLatestByDeviceIdAsync(int deviceId);

    Task<MeasurementResponse> CreateAsync(int deviceId, CreateMeasurementRequest request);
}
