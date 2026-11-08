using IoTPlatform.Application.DTOs.Alerts;
using IoTPlatform.Application.DTOs.Common;
using IoTPlatform.Domain.Enums;

namespace IoTPlatform.Application.Interfaces;

public interface IAlertService
{
    Task<PagedResult<AlertResponse>> GetAllAsync(
        int? deviceId = null,
        AlertSeverity? severity = null,
        bool? isAcknowledged = null,
        int pageNumber = 1,
        int pageSize = 10);

    Task<int> GetUnacknowledgedCountAsync(int? deviceId = null);

    Task<AlertResponse?> AcknowledgeAsync(int id);

    Task<AlertResponse> CreateAsync(CreateAlertRequest request);
}
