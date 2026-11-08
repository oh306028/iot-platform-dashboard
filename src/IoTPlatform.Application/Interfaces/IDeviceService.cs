using IoTPlatform.Application.DTOs.Devices;

namespace IoTPlatform.Application.Interfaces;

public interface IDeviceService
{
    Task<IEnumerable<DeviceResponse>> GetAllAsync();
    Task<DeviceResponse?> GetByIdAsync(int id);
    Task<DeviceResponse> CreateAsync(CreateDeviceRequest request);
    Task<DeviceResponse?> UpdateAsync(int id, UpdateDeviceRequest request);
    Task<bool> DeleteAsync(int id);
}
