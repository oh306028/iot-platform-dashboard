using System.ComponentModel.DataAnnotations;

namespace IoTPlatform.Domain.Enums;

public enum AlertSeverity
{
    [Display(Name = "Informacja")]
    Info,

    [Display(Name = "Ostrzeżenie")]
    Warning,

    [Display(Name = "Krytyczny")]
    Critical
}
