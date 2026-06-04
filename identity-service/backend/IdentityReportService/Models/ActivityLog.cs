using System.ComponentModel.DataAnnotations;

namespace IdentityReportService.Models;

public class ActivityLog
{
  public int Id { get; set; }
  [Required, MaxLength(120)] public string Action { get; set; } = string.Empty;
  [MaxLength(500)] public string Description { get; set; } = string.Empty;
  [MaxLength(160)] public string UserEmail { get; set; } = string.Empty;
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  [MaxLength(50)] public string Type { get; set; } = "info";
}
