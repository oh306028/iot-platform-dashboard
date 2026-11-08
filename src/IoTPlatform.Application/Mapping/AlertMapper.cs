using IoTPlatform.Application.DTOs.Alerts;
using IoTPlatform.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace IoTPlatform.Application.Mapping;

[Mapper]
public partial class AlertMapper
{
    [MapProperty(nameof(Alert.Device.Name), nameof(AlertResponse.DeviceName))]
    public partial AlertResponse ToResponse(Alert alert);

    public partial IEnumerable<AlertResponse> ToResponseList(IEnumerable<Alert> alerts);
}
