using IoTPlatform.Application.DTOs.Devices;
using IoTPlatform.Application.Interfaces;
using IoTPlatform.Application.Mapping;
using IoTPlatform.Domain.Entities;
using IoTPlatform.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IoTPlatform.Infrastructure.Services;

public class DeviceService : IDeviceService
{
    private readonly AppDbContext _context;
    private readonly DeviceMapper _mapper = new();

    public DeviceService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DeviceResponse>> GetAllAsync()
    {
        var devices = await _context.Devices.AsNoTracking().ToListAsync();
        return _mapper.ToResponseList(devices);
    }

    public async Task<DeviceResponse?> GetByIdAsync(int id)
    {
        var device = await _context.Devices.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
        return device is null ? null : _mapper.ToResponse(device);
    }

    public async Task<DeviceResponse> CreateAsync(CreateDeviceRequest request)
    {
        if (await _context.Devices.AnyAsync(d => d.ClientIdentifier == request.ClientIdentifier))
            throw new InvalidOperationException("Device with this ClientIdentifier already exists.");

        var device = new Device
        {
            Name = request.Name,
            ClientIdentifier = request.ClientIdentifier,
            Description = request.Description,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.Devices.Add(device);
        await _context.SaveChangesAsync();

        return _mapper.ToResponse(device);
    }

    public async Task<DeviceResponse?> UpdateAsync(int id, UpdateDeviceRequest request)
    {
        var device = await _context.Devices.FirstOrDefaultAsync(d => d.Id == id);
        if (device is null)
            return null;

        device.Name = request.Name;
        device.Description = request.Description;
        device.IsActive = request.IsActive;

        await _context.SaveChangesAsync();

        return _mapper.ToResponse(device);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var device = await _context.Devices.FirstOrDefaultAsync(d => d.Id == id);
        if (device is null)
            return false;

        _context.Devices.Remove(device);
        await _context.SaveChangesAsync();

        return true;
    }
}
