using IdentityReportService.Data;
using IdentityReportService.DTOs;
using IdentityReportService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityReportService.Controllers;

[ApiController, Route("api/cards")]
public class CardsController(AppDbContext db) : ControllerBase
{
  [HttpGet, Authorize(Roles = "Admin,Librarian")]
  public async Task<IEnumerable<LibraryCardDto>> GetAll() =>
    await db.LibraryCards
      .AsNoTracking()
      .Include(c => c.User)
      .OrderByDescending(c => c.Id)
      .Select(c => new LibraryCardDto(c.Id, c.UserId, c.User!.FullName, c.CardNumber, c.Status, c.IssuedAt, c.ExpiredAt))
      .ToListAsync();

  [HttpPost, Authorize(Roles = "Admin,Librarian")]
  public async Task<IActionResult> Create(CreateLibraryCardDto dto)
  {
    var user = await db.Users.Include(u => u.LibraryCard).FirstOrDefaultAsync(x => x.Id == dto.UserId);
    if (user == null) return NotFound(new { message = "Không tìm thấy độc giả" });
    if (user.Role != "Reader") return BadRequest(new { message = "Chỉ độc giả mới được cấp thẻ" });
    if (user.LibraryCard != null) return BadRequest(new { message = "Độc giả đã có thẻ thư viện" });

    var card = new LibraryCard
    {
      UserId = user.Id,
      CardNumber = $"CARD-{user.Id:0000}-{DateTime.UtcNow:HHmmss}",
      Status = "Active"
    };
    db.LibraryCards.Add(card);
    await db.SaveChangesAsync();
    return Ok(new LibraryCardDto(card.Id, user.Id, user.FullName, card.CardNumber, card.Status, card.IssuedAt, card.ExpiredAt));
  }

  [HttpPut("{id:int}/lock"), Authorize(Roles = "Admin,Librarian")]
  public async Task<IActionResult> Lock(int id) => await Change(id, "Locked");

  [HttpPut("{id:int}/unlock"), Authorize(Roles = "Admin,Librarian")]
  public async Task<IActionResult> Unlock(int id) => await Change(id, "Active");

  [HttpPut("{id:int}/renew"), Authorize(Roles = "Admin,Librarian")]
  public async Task<IActionResult> Renew(int id)
  {
    var c = await db.LibraryCards.FindAsync(id);
    if (c == null) return NotFound(new { message = "Không tìm thấy thẻ" });
    c.Status = "Active";
    c.ExpiredAt = DateTime.UtcNow.AddYears(1);
    await db.SaveChangesAsync();
    return Ok(new { message = "Đã gia hạn thẻ" });
  }

  [HttpGet("validate/{readerId:int}")]
  public async Task<ActionResult<ValidateCardResponse>> Validate(int readerId)
  {
    var c = await db.LibraryCards.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == readerId);
    if (c == null) return Ok(new ValidateCardResponse(false, "None", "Độc giả chưa có thẻ"));
    var valid = c.Status == "Active" && c.ExpiredAt > DateTime.UtcNow;
    return Ok(new ValidateCardResponse(valid, c.Status, valid ? "Thẻ hợp lệ" : "Thẻ bị khóa hoặc hết hạn"));
  }

  private async Task<IActionResult> Change(int id, string status)
  {
    var c = await db.LibraryCards.FindAsync(id);
    if (c == null) return NotFound(new { message = "Không tìm thấy thẻ" });
    c.Status = status;
    await db.SaveChangesAsync();
    return Ok(new { message = $"Đã chuyển trạng thái {status}" });
  }
}
