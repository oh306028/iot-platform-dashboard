namespace IoTPlatform.Application.DTOs.Devices;

public class UpdateDeviceRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}
