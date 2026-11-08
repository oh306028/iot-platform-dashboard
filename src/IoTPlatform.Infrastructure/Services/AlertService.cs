using IoTPlatform.Application.DTOs.Alerts;
using IoTPlatform.Application.DTOs.Common;
using IoTPlatform.Application.Interfaces;
using IoTPlatform.Application.Mapping;
using IoTPlatform.Domain.Entities;
using IoTPlatform.Domain.Enums;
using IoTPlatform.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IoTPlatform.Infrastructure.Services;

public class AlertService : IAlertService
{
    private readonly AppDbContext _context;
    private readonly AlertMapper _mapper = new();

    public AlertService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<AlertResponse>> GetAllAsync(
        int? deviceId = null,
        AlertSeverity? severity = null,
        bool? isAcknowledged = null,
        int pageNumber = 1,
        int pageSize = 10)
    {
        if (pageNumber < 1)
            pageNumber = 1;

        if (pageSize < 1)
            pageSize = 10;

        var query = _context.Alerts
            .Include(a => a.Device)
            .AsNoTracking();

        if (deviceId.HasValue)
            query = query.Where(a => a.DeviceId == deviceId.Value);

        if (severity.HasValue)
            query = query.Where(a => a.Severity == severity.Value);

        if (isAcknowledged.HasValue)
            query = query.Where(a => a.IsAcknowledged == isAcknowledged.Value);

        var totalCount = await query.CountAsync();

        var alerts = await query
            .OrderByDescending(a => a.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<AlertResponse>
        {
            Items = _mapper.ToResponseList(alerts),
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

    public async Task<int> GetUnacknowledgedCountAsync(int? deviceId = null)
    {
        var query = _context.Alerts.AsNoTracking().Where(a => !a.IsAcknowledged);

        if (deviceId.HasValue)
            query = query.Where(a => a.DeviceId == deviceId.Value);

        return await query.CountAsync();
    }

    public async Task<AlertResponse?> AcknowledgeAsync(int id)
    {
        var alert = await _context.Alerts
            .Include(a => a.Device)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (alert is null)
            return null;

        alert.IsAcknowledged = true;
        await _context.SaveChangesAsync();

        return _mapper.ToResponse(alert);
    }

    public async Task<AlertResponse> CreateAsync(CreateAlertRequest request)
    {
        if (request.DeviceId.HasValue)
        {
            var deviceExists = await _context.Devices.AnyAsync(d => d.Id == request.DeviceId.Value);
            if (!deviceExists)
                throw new KeyNotFoundException($"Device with id {request.DeviceId.Value} was not found.");
        }

        var alert = new Alert
        {
            DeviceId = request.DeviceId,
            Severity = request.Severity,
            Message = request.Message,
            IsAcknowledged = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.Alerts.Add(alert);
        await _context.SaveChangesAsync();

        if (alert.DeviceId.HasValue)
        {
            await _context.Entry(alert).Reference(a => a.Device).LoadAsync();
        }

        return _mapper.ToResponse(alert);
    }
}
