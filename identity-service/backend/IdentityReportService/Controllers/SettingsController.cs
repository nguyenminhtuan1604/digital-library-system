using IdentityReportService.Data;
using IdentityReportService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityReportService.Controllers;

[ApiController, Route("api/settings"), Authorize(Roles = "Admin")]
public class SettingsController(AppDbContext db) : ControllerBase
{
  [HttpGet]
  public async Task<ActionResult<SystemSettings>> Get()
  {
    var settings = await db.SystemSettings.OrderBy(x => x.Id).FirstOrDefaultAsync();
    if (settings == null)
    {
      settings = new SystemSettings();
      db.SystemSettings.Add(settings);
      await db.SaveChangesAsync();
    }

    if (Normalize(settings))
    {
      await db.SaveChangesAsync();
    }

    Response.ContentType = "application/json; charset=utf-8";
    return Ok(settings);
  }

  [HttpPut]
  public async Task<ActionResult<SystemSettings>> Save(SystemSettings dto)
  {
    var settings = await db.SystemSettings.OrderBy(x => x.Id).FirstOrDefaultAsync();
    if (settings == null)
    {
      settings = new SystemSettings();
      db.SystemSettings.Add(settings);
    }

    settings.SystemName = Clean(dto.SystemName, "Thư viện số");
    settings.Language = Clean(dto.Language, "Tiếng Việt");
    settings.TimeZone = string.IsNullOrWhiteSpace(dto.TimeZone) ? "Asia/Ho_Chi_Minh" : dto.TimeZone;
    settings.EnableRealtimeNotification = dto.EnableRealtimeNotification;
    settings.ThemeMode = NormalizeTheme(dto.ThemeMode);
    settings.PrimaryColor = NormalizeColor(dto.PrimaryColor);
    settings.BorderRadius = dto.BorderRadius <= 0 ? 14 : dto.BorderRadius;
    settings.CollapseSidebar = dto.CollapseSidebar;
    settings.RequireAdminVerification = dto.RequireAdminVerification;
    settings.EnableActivityLog = dto.EnableActivityLog;
    settings.AutoLogoutMinutes = dto.AutoLogoutMinutes;
    settings.NotifyExpiringCards = dto.NotifyExpiringCards;
    settings.NotifyOverdueBooks = dto.NotifyOverdueBooks;
    settings.NotifyNewUsers = dto.NotifyNewUsers;
    settings.NotifyByEmail = dto.NotifyByEmail;
    settings.UpdatedAt = DateTime.UtcNow;

    await db.SaveChangesAsync();
    Response.ContentType = "application/json; charset=utf-8";
    return Ok(settings);
  }

  private static bool Normalize(SystemSettings settings)
  {
    var changed = false;
    if (LooksCorrupted(settings.SystemName) || string.IsNullOrWhiteSpace(settings.SystemName))
    {
      settings.SystemName = "Thư viện số";
      changed = true;
    }
    if (LooksCorrupted(settings.Language) || string.IsNullOrWhiteSpace(settings.Language))
    {
      settings.Language = "Tiếng Việt";
      changed = true;
    }
    if (settings.TimeZone == "Asia/Saigon" || string.IsNullOrWhiteSpace(settings.TimeZone))
    {
      settings.TimeZone = "Asia/Ho_Chi_Minh";
      changed = true;
    }
    var theme = NormalizeTheme(settings.ThemeMode);
    if (theme != settings.ThemeMode)
    {
      settings.ThemeMode = theme;
      changed = true;
    }
    var color = NormalizeColor(settings.PrimaryColor);
    if (color != settings.PrimaryColor)
    {
      settings.PrimaryColor = color;
      changed = true;
    }
    if (settings.BorderRadius <= 0)
    {
      settings.BorderRadius = 14;
      changed = true;
    }
    if (changed) settings.UpdatedAt = DateTime.UtcNow;
    return changed;
  }

  private static string Clean(string value, string fallback) =>
    LooksCorrupted(value) || string.IsNullOrWhiteSpace(value) ? fallback : value.Trim();

  private static string NormalizeTheme(string value) =>
    value == "dark" || value == "Tối" ? "Tối" : "Sáng";

  private static string NormalizeColor(string value) =>
    value switch
    {
      "Xanh dương" => "Xanh dương",
      "Tím" => "Tím",
      "Xanh lá" => "Xanh lá",
      "#3B82F6" => "Xanh dương",
      "#A78BFA" => "Tím",
      "#22C55E" => "Xanh lá",
      _ => "Xanh pastel"
    };

  private static bool LooksCorrupted(string? value) =>
    string.IsNullOrEmpty(value) || value.Contains('?') || value.Contains('Æ') || value.Contains('á') || value.Contains('»') || value.Contains('Ã');
}
