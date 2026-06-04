using System.ComponentModel.DataAnnotations;

namespace IdentityReportService.Models;

public class ReturnEvent
{
  public int Id { get; set; }
  public int ReaderId { get; set; }
  public int BookId { get; set; }
  [MaxLength(200)] public string BookTitle { get; set; } = string.Empty;
  public DateTime ReturnedAt { get; set; } = DateTime.UtcNow;
  public decimal FineAmount { get; set; }
}
