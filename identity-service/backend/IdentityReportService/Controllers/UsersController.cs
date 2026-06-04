using System.Security.Claims;
using System.Text;
using IdentityReportService.Data;
using IdentityReportService.DTOs;
using IdentityReportService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityReportService.Controllers;

[ApiController, Route("api/users"), Authorize(Roles = "Admin,Librarian")]
public class UsersController(AppDbContext db) : ControllerBase
{
  [HttpGet]
  public async Task<IEnumerable<UserDto>> GetAll() =>
    await db.Users
      .AsNoTracking()
      .OrderByDescending(u => u.Id)
      .Select(u => new UserDto(u.Id, u.FullName, u.Email, u.Role, u.IsActive))
      .ToListAsync();

  [HttpGet("{id:int}")]
  public async Task<ActionResult<UserDto>> GetById(int id)
  {
    var u = await db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    if (u == null) return NotFound(new { message = "Không tìm thấy người dùng" });
    return new UserDto(u.Id, u.FullName, u.Email, u.Role, u.IsActive);
  }

  [HttpGet("{id:int}/profile")]
  [AllowAnonymous]
  public async Task<IActionResult> Profile(int id)
  {
    var user = await db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    if (user == null) return NotFound(new { message = "Không tìm thấy người dùng" });

    var card = await db.LibraryCards.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == id);
    var borrowEvents = db.BorrowEvents.AsNoTracking().Where(x => x.ReaderId == id);
    var returnEvents = db.ReturnEvents.AsNoTracking().Where(x => x.ReaderId == id);
    var now = DateTime.UtcNow;

    return Ok(new
    {
      user.Id,
      user.FullName,
      user.Email,
      user.Role,
      status = user.IsActive ? "Active" : "Locked",
      libraryCard = card == null ? null : new
      {
        cardCode = card.CardNumber,
        card.Status,
        issueDate = card.IssuedAt,
        expiryDate = card.ExpiredAt
      },
      borrowSummary = new
      {
        totalBorrowed = await borrowEvents.CountAsync(),
        totalReturned = await returnEvents.CountAsync(),
        overdueBooks = await borrowEvents.CountAsync(x => !x.IsReturned && x.BorrowedAt.AddDays(14) < now),
        totalFines = await returnEvents.SumAsync(x => x.FineAmount)
      }
    });
  }

  [HttpPost]
  public async Task<ActionResult<UserDto>> Create(CreateUserDto dto)
  {
    if (string.IsNullOrWhiteSpace(dto.FullName) ||
        string.IsNullOrWhiteSpace(dto.Email) ||
        string.IsNullOrWhiteSpace(dto.Password))
    {
      return BadRequest(new { message = "Vui lòng nhập đầy đủ thông tin" });
    }

    if (dto.Password.Length < 6) return BadRequest(new { message = "Mật khẩu tối thiểu 6 ký tự" });
    if (await db.Users.AnyAsync(x => x.Email == dto.Email)) return BadRequest(new { message = "Email đã tồn tại" });

    var role = NormalizeRole(dto.Role);
    var user = new User
    {
      FullName = dto.FullName.Trim(),
      Email = dto.Email.Trim(),
      PasswordHash = AppDbContext.Hash(dto.Password),
      Role = role,
      IsActive = dto.IsActive
    };

    db.Users.Add(user);
    await db.SaveChangesAsync();

    if (role == "Reader")
    {
      db.LibraryCards.Add(new LibraryCard
      {
        UserId = user.Id,
        CardNumber = $"CARD-{user.Id:0000}",
        Status = "Active"
      });
      await db.SaveChangesAsync();
    }

    return CreatedAtAction(nameof(GetById), new { id = user.Id }, new UserDto(user.Id, user.FullName, user.Email, user.Role, user.IsActive));
  }

  [HttpPut("{id:int}")]
  public async Task<IActionResult> Update(int id, UpdateUserDto dto)
  {
    var user = await db.Users.FindAsync(id);
    if (user == null) return NotFound(new { message = "Không tìm thấy người dùng" });

    var email = dto.Email.Trim();
    if (await db.Users.AnyAsync(x => x.Id != id && x.Email == email)) return BadRequest(new { message = "Email đã tồn tại" });

    user.FullName = dto.FullName.Trim();
    user.Email = email;
    user.Role = NormalizeRole(dto.Role);
    user.IsActive = dto.IsActive;
    if (!string.IsNullOrWhiteSpace(dto.Password))
    {
      if (dto.Password.Length < 6) return BadRequest(new { message = "Mật khẩu tối thiểu 6 ký tự" });
      user.PasswordHash = AppDbContext.Hash(dto.Password);
    }

    await db.SaveChangesAsync();
    return Ok(new UserDto(user.Id, user.FullName, user.Email, user.Role, user.IsActive));
  }

  [HttpDelete("{id:int}")]
  public async Task<IActionResult> Delete(int id)
  {
    if (!User.IsInRole("Admin")) return Forbid();

    var user = await db.Users.Include(u => u.LibraryCard).FirstOrDefaultAsync(u => u.Id == id);
    if (user == null) return NotFound(new { message = "Không tìm thấy người dùng" });
    if (user.Role == "Admin" && await db.Users.CountAsync(u => u.Role == "Admin") <= 1)
    {
      return BadRequest(new { message = "Không thể xóa admin cuối cùng" });
    }

    db.Users.Remove(user);
    await db.SaveChangesAsync();
    return Ok(new { message = "Đã xóa người dùng" });
  }

  [HttpPut("{id:int}/status")]
  public async Task<IActionResult> SetStatus(int id, [FromQuery] bool active)
  {
    var u = await db.Users.FindAsync(id);
    if (u == null) return NotFound(new { message = "Không tìm thấy người dùng" });
    u.IsActive = active;
    await db.SaveChangesAsync();
    return Ok(new { message = "Đã cập nhật trạng thái" });
  }

  [HttpPost("import")]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> Import(IFormFile file)
  {
    if (file == null || file.Length == 0) return BadRequest(new { message = "Vui lòng chọn file CSV" });

    var imported = 0;
    var skipped = 0;

    using var reader = new StreamReader(file.OpenReadStream(), Encoding.UTF8);
    var header = await reader.ReadLineAsync();
    if (string.IsNullOrWhiteSpace(header)) return BadRequest(new { message = "File CSV không hợp lệ" });

    while (!reader.EndOfStream)
    {
      var line = await reader.ReadLineAsync();
      if (string.IsNullOrWhiteSpace(line)) continue;

      var columns = ParseCsvLine(line);
      if (columns.Count < 5)
      {
        skipped++;
        continue;
      }

      var fullName = columns[0].Trim();
      var email = columns[1].Trim();
      var password = columns[2].Trim();
      var role = NormalizeRole(columns[3].Trim());
      var status = NormalizeStatus(columns[4].Trim());

      if (string.IsNullOrWhiteSpace(fullName) ||
          string.IsNullOrWhiteSpace(email) ||
          password.Length < 6 ||
          !new[] { "Admin", "Librarian", "Reader" }.Contains(role))
      {
        skipped++;
        continue;
      }

      var existing = await db.Users.FirstOrDefaultAsync(x => x.Email == email);
      if (existing == null)
      {
        var user = new User
        {
          FullName = fullName,
          Email = email,
          PasswordHash = AppDbContext.Hash(password),
          Role = role,
          IsActive = status == "Active"
        };
        db.Users.Add(user);
        imported++;
      }
      else
      {
        existing.FullName = fullName;
        existing.Role = role;
        existing.IsActive = status == "Active";
        if (!string.IsNullOrWhiteSpace(password)) existing.PasswordHash = AppDbContext.Hash(password);
        skipped++;
      }
    }

    await db.SaveChangesAsync();
    return Ok(new { imported, skipped, message = "Nhập người dùng thành công" });
  }

  [HttpGet("export")]
  public async Task<IActionResult> Export()
  {
    var users = await db.Users.AsNoTracking().OrderBy(x => x.Id).ToListAsync();
    var csv = new StringBuilder();
    csv.AppendLine("Id,FullName,Email,Role,Status,CreatedAt");
    foreach (var user in users)
    {
      csv.AppendLine($"{user.Id},{Escape(user.FullName)},{Escape(user.Email)},{user.Role},{(user.IsActive ? "Active" : "Locked")},{user.CreatedAt:yyyy-MM-dd HH:mm:ss}");
    }

    return File(Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(csv.ToString())).ToArray(), "text/csv", "users-export.csv");
  }

  private static string NormalizeRole(string role) =>
    new[] { "Admin", "Librarian", "Reader" }.Contains(role) ? role : "Reader";

  private static string NormalizeStatus(string status) =>
    status.Equals("Locked", StringComparison.OrdinalIgnoreCase) ? "Locked" : "Active";

  private static List<string> ParseCsvLine(string line)
  {
    var values = new List<string>();
    var current = new StringBuilder();
    var inQuotes = false;

    for (var i = 0; i < line.Length; i++)
    {
      var c = line[i];
      if (c == '"' && i + 1 < line.Length && line[i + 1] == '"')
      {
        current.Append('"');
        i++;
      }
      else if (c == '"')
      {
        inQuotes = !inQuotes;
      }
      else if (c == ',' && !inQuotes)
      {
        values.Add(current.ToString());
        current.Clear();
      }
      else
      {
        current.Append(c);
      }
    }

    values.Add(current.ToString());
    return values;
  }

  private static string Escape(string value) =>
    value.Contains(',') || value.Contains('"') || value.Contains('\n')
      ? $"\"{value.Replace("\"", "\"\"")}\""
      : value;
}
