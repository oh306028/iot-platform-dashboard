using System.ComponentModel.DataAnnotations;

namespace IoTPlatform.Domain.Enums;

public enum MetricType
{
    [Display(Name = "Temperatura")]
    Temperature,

    [Display(Name = "Wilgotność")]
    Humidity,

    [Display(Name = "Ciśnienie")]
    Pressure,

    [Display(Name = "Napięcie")]
    Voltage,

    [Display(Name = "Poziom baterii")]
    BatteryLevel,

    [Display(Name = "Jakość powietrza")]
    AirQuality,

    [Display(Name = "Zużycie energii")]
    PowerConsumption
}
