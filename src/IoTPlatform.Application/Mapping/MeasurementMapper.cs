using IoTPlatform.Application.DTOs.Measurements;
using IoTPlatform.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace IoTPlatform.Application.Mapping;

[Mapper]
public partial class MeasurementMapper
{
    [MapperIgnoreSource(nameof(Measurement.Device))]
    public partial MeasurementResponse ToResponse(Measurement measurement);

    public partial IEnumerable<MeasurementResponse> ToResponseList(IEnumerable<Measurement> measurements);
}
