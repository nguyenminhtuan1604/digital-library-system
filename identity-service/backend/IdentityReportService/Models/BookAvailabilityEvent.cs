using System.ComponentModel.DataAnnotations;

namespace IdentityReportService.Models;

public class BookAvailabilityEvent
{
  public int Id { get; set; }
  [Required, MaxLength(50)] public string BookId { get; set; } = string.Empty;
  [Required, MaxLength(200)] public string BookTitle { get; set; } = string.Empty;
  [MaxLength(120)] public string Category { get; set; } = string.Empty;
  public int AvailableCopies { get; set; }
  public int TotalCopies { get; set; }
  public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
}
