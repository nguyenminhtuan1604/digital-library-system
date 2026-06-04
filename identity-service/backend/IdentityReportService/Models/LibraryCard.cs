using System.ComponentModel.DataAnnotations;
namespace IdentityReportService.Models;
public class LibraryCard {
  public int Id { get; set; }
  public int UserId { get; set; }
  public User? User { get; set; }
  [Required, MaxLength(50)] public string CardNumber { get; set; } = string.Empty;
  [Required, MaxLength(20)] public string Status { get; set; } = "Active"; // Active, Locked, Expired
  public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
  public DateTime ExpiredAt { get; set; } = DateTime.UtcNow.AddYears(1);
}
