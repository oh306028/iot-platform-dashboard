using IoTPlatform.Application.DTOs.Common;
using IoTPlatform.Application.DTOs.Measurements;
using IoTPlatform.Application.Interfaces;
using IoTPlatform.Application.Mapping;
using IoTPlatform.Domain.Entities;
using IoTPlatform.Domain.Enums;
using IoTPlatform.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IoTPlatform.Infrastructure.Services;

public class MeasurementService : IMeasurementService
{
    private readonly AppDbContext _context;
    private readonly MeasurementMapper _mapper = new();

    public MeasurementService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<MeasurementResponse>> GetByDeviceIdAsync(
        int deviceId,
        MetricType? metricType = null,
        DateTime? from = null,
        DateTime? to = null,
        int pageNumber = 1,
        int pageSize = 10)
    {
        if (pageNumber < 1)
            pageNumber = 1;

        if (pageSize < 1)
            pageSize = 10;

        var query = _context.Measurements
            .AsNoTracking()
            .Where(m => m.DeviceId == deviceId);

        if (metricType.HasValue)
            query = query.Where(m => m.MetricType == metricType.Value);

        if (from.HasValue)
            query = query.Where(m => m.Timestamp >= from.Value);

        if (to.HasValue)
            query = query.Where(m => m.Timestamp <= to.Value);

        var totalCount = await query.CountAsync();

        var measurements = await query
            .OrderByDescending(m => m.Timestamp)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<MeasurementResponse>
        {
            Items = _mapper.ToResponseList(measurements),
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

    public async Task<IEnumerable<MeasurementResponse>> GetLatestByDeviceIdAsync(int deviceId)
    {
        var latestMeasurements = await _context.Measurements
            .AsNoTracking()
            .Where(m => m.DeviceId == deviceId)
            .GroupBy(m => m.MetricType)
            .Select(g => g.OrderByDescending(m => m.Timestamp).First())
            .ToListAsync();

        return _mapper.ToResponseList(latestMeasurements);
    }

    public async Task<MeasurementResponse> CreateAsync(int deviceId, CreateMeasurementRequest request)
    {
        var device = await _context.Devices.FirstOrDefaultAsync(d => d.Id == deviceId);
        if (device is null)
            throw new KeyNotFoundException($"Device with id {deviceId} was not found.");

        var measurement = new Measurement
        {
            DeviceId = deviceId,
            MetricType = request.MetricType,
            Value = request.Value,
            Unit = request.Unit,
            Timestamp = request.Timestamp ?? DateTime.UtcNow
        };

        _context.Measurements.Add(measurement);

        EvaluateMeasurementRules(device, measurement);

        await _context.SaveChangesAsync();

        return _mapper.ToResponse(measurement);
    }

    private void EvaluateMeasurementRules(Device device, Measurement measurement)
    {
        if (measurement.MetricType == MetricType.Temperature)
        {
            if (measurement.Value >= 30.0)
            {
                _context.Alerts.Add(new Alert
                {
                    DeviceId = device.Id,
                    Severity = AlertSeverity.Critical,
                    Message = $"Krytyczna temperatura na urządzeniu '{device.Name}': {measurement.Value}{measurement.Unit} (próg: >= 30{measurement.Unit})",
                    IsAcknowledged = false,
                    CreatedAt = DateTime.UtcNow
                });
            }
            else if (measurement.Value >= 25.0)
            {
                _context.Alerts.Add(new Alert
                {
                    DeviceId = device.Id,
                    Severity = AlertSeverity.Warning,
                    Message = $"Ostrzeżenie o wysokiej temperaturze na urządzeniu '{device.Name}': {measurement.Value}{measurement.Unit} (próg: >= 25{measurement.Unit})",
                    IsAcknowledged = false,
                    CreatedAt = DateTime.UtcNow
                });
            }
        }
    }
}
