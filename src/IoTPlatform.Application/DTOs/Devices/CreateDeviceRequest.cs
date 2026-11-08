namespace IoTPlatform.Application.DTOs.Devices;

public class CreateDeviceRequest
{
    public string Name { get; set; } = string.Empty;
    public string ClientIdentifier { get; set; } = string.Empty;
    public string? Description { get; set; }
}
