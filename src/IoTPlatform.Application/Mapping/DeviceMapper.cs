using IoTPlatform.Application.DTOs.Devices;
using IoTPlatform.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace IoTPlatform.Application.Mapping;

[Mapper]
public partial class DeviceMapper
{
    [MapperIgnoreSource(nameof(Device.Measurements))]
    [MapperIgnoreSource(nameof(Device.Alerts))]
    [MapperIgnoreSource(nameof(Device.Logs))]
    public partial DeviceResponse ToResponse(Device device);

    public partial IEnumerable<DeviceResponse> ToResponseList(IEnumerable<Device> devices);
}
