using IdentityReportService.Data;
using IdentityReportService.DTOs;
using IdentityReportService.Models;
using IdentityReportService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityReportService.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(AppDbContext db, JwtService jwt) : ControllerBase
{
  [HttpPost("register")]
  public async Task<IActionResult> Register(RegisterDto dto)
  {
    if (string.IsNullOrWhiteSpace(dto.FullName) ||
        string.IsNullOrWhiteSpace(dto.Email) ||
        string.IsNullOrWhiteSpace(dto.Password))
    {
      return BadRequest(new { message = "Vui lòng nhập đầy đủ thông tin" });
    }

    if (dto.Password.Length < 6) return BadRequest(new { message = "Mật khẩu tối thiểu 6 ký tự" });
    if (await db.Users.AnyAsync(x => x.Email == dto.Email)) return BadRequest(new { message = "Email đã tồn tại" });

    var role = new[] { "Admin", "Librarian", "Reader" }.Contains(dto.Role) ? dto.Role : "Reader";
    var user = new User
    {
      FullName = dto.FullName,
      Email = dto.Email,
      PasswordHash = AppDbContext.Hash(dto.Password),
      Role = role
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

    return Ok(new { message = "Đăng ký thành công" });
  }

  [HttpPost("login")]
  public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto)
  {
    var hash = AppDbContext.Hash(dto.Password);
    var user = await db.Users.FirstOrDefaultAsync(x => x.Email == dto.Email && x.PasswordHash == hash && x.IsActive);
    if (user == null) return Unauthorized(new { message = "Sai email hoặc mật khẩu" });

    return Ok(new AuthResponseDto(
      jwt.Generate(user),
      new AuthUserDto(user.Id, user.FullName, user.Email, user.Role)
    ));
  }
}
