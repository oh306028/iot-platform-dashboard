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

    public async Task<IEnumerable<MeasurementResponse>> GetByDeviceIdAsync(
        int deviceId,
        MetricType? metricType = null,
        DateTime? from = null,
        DateTime? to = null,
        int limit = 100)
    {
        var query = _context.Measurements
            .AsNoTracking()
            .Where(m => m.DeviceId == deviceId);

        if (metricType.HasValue)
            query = query.Where(m => m.MetricType == metricType.Value);

        if (from.HasValue)
            query = query.Where(m => m.Timestamp >= from.Value);

        if (to.HasValue)
            query = query.Where(m => m.Timestamp <= to.Value);

        var measurements = await query
            .OrderByDescending(m => m.Timestamp)
            .Take(limit)
            .ToListAsync();

        return _mapper.ToResponseList(measurements);
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
        var deviceExists = await _context.Devices.AnyAsync(d => d.Id == deviceId);
        if (!deviceExists)
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
        await _context.SaveChangesAsync();

        return _mapper.ToResponse(measurement);
    }
}
