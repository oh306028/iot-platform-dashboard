namespace IoTPlatform.Application.DTOs.Devices;

public class DeviceResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ClientIdentifier { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
}
