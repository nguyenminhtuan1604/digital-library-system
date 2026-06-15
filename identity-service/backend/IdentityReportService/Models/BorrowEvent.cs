using System.ComponentModel.DataAnnotations;
namespace IdentityReportService.Models;
public class BorrowEvent {
  public int Id { get; set; }
  [Required] public string EventType { get; set; } = "book.borrowed";
  public int ReaderId { get; set; }
  public int BookId { get; set; }
  [MaxLength(200)] public string BookTitle { get; set; } = string.Empty;
  public DateTime BorrowedAt { get; set; } = DateTime.UtcNow;
  public DateTime? ReturnedAt { get; set; }
  public bool IsReturned { get; set; }
  public decimal FineAmount { get; set; }
}
