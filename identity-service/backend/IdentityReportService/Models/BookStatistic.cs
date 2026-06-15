namespace IdentityReportService.Models;
public class BookStatistic {
  public int Id { get; set; }
  public int BookId { get; set; }
  public string BookTitle { get; set; } = string.Empty;
  public int TotalBorrowed { get; set; }
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
