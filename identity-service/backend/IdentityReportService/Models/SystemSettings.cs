using System.ComponentModel.DataAnnotations;

namespace IdentityReportService.Models;

public class SystemSettings
{
  public int Id { get; set; }
  [MaxLength(160)] public string SystemName { get; set; } = "Thư viện số";
  [MaxLength(40)] public string Language { get; set; } = "Tiếng Việt";
  [MaxLength(80)] public string TimeZone { get; set; } = "Asia/Ho_Chi_Minh";
  public bool EnableRealtimeNotification { get; set; } = true;
  [MaxLength(30)] public string ThemeMode { get; set; } = "Sáng";
  [MaxLength(30)] public string PrimaryColor { get; set; } = "Xanh pastel";
  public int BorderRadius { get; set; } = 14;
  public bool CollapseSidebar { get; set; }
  public bool RequireAdminVerification { get; set; } = true;
  public bool EnableActivityLog { get; set; } = true;
  public int AutoLogoutMinutes { get; set; } = 30;
  public bool NotifyExpiringCards { get; set; } = true;
  public bool NotifyOverdueBooks { get; set; } = true;
  public bool NotifyNewUsers { get; set; } = true;
  public bool NotifyByEmail { get; set; }
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
