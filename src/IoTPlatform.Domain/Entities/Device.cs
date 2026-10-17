namespace IoTPlatform.Domain.Entities;

public class Device
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ClientIdentifier { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Measurement> Measurements { get; set; } = new List<Measurement>();
    public ICollection<Alert> Alerts { get; set; } = new List<Alert>();
}
