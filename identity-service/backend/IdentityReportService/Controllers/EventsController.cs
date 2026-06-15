using IdentityReportService.Data;
using IdentityReportService.DTOs;
using IdentityReportService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityReportService.Controllers;

[ApiController, Route("api/events"), Authorize(Roles = "Admin,Librarian")]
public class EventsController(AppDbContext db) : ControllerBase
{
  [HttpPost("book-borrowed")]
  public async Task<IActionResult> BookBorrowed(BorrowedEventDto dto)
  {
    var borrowedAt = dto.BorrowedAt ?? DateTime.UtcNow;
    db.BorrowEvents.Add(new BorrowEvent
    {
      EventType = "book.borrowed",
      ReaderId = dto.ReaderId,
      BookId = dto.BookId,
      BookTitle = dto.BookTitle,
      BorrowedAt = borrowedAt
    });

    var stat = await db.BookStatistics.FirstOrDefaultAsync(x => x.BookId == dto.BookId);
    if (stat == null)
    {
      db.BookStatistics.Add(new BookStatistic { BookId = dto.BookId, BookTitle = dto.BookTitle, TotalBorrowed = 1 });
    }
    else
    {
      stat.TotalBorrowed++;
      stat.BookTitle = dto.BookTitle;
      stat.UpdatedAt = DateTime.UtcNow;
    }

    db.ActivityLogs.Add(new ActivityLog
    {
      Action = "Mượn sách",
      Description = $"Độc giả #{dto.ReaderId} đã mượn {dto.BookTitle}",
      UserEmail = User.Identity?.Name ?? "system",
      Type = "borrow",
      CreatedAt = borrowedAt
    });

    await db.SaveChangesAsync();
    return Ok(new { message = "Đã nhận event mượn sách" });
  }

  [HttpPost("book-returned")]
  public async Task<IActionResult> BookReturned(ReturnedEventDto dto)
  {
    var returnedAt = dto.ReturnedAt ?? DateTime.UtcNow;
    var borrow = await db.BorrowEvents
      .Where(x => x.ReaderId == dto.ReaderId && x.BookId == dto.BookId && !x.IsReturned)
      .OrderByDescending(x => x.BorrowedAt)
      .FirstOrDefaultAsync();

    if (borrow == null) return NotFound(new { message = "Không tìm thấy lượt mượn" });

    borrow.EventType = "book.returned";
    borrow.IsReturned = true;
    borrow.ReturnedAt = returnedAt;
    borrow.FineAmount = dto.FineAmount;

    db.ReturnEvents.Add(new ReturnEvent
    {
      ReaderId = dto.ReaderId,
      BookId = dto.BookId,
      BookTitle = borrow.BookTitle,
      ReturnedAt = returnedAt,
      FineAmount = dto.FineAmount
    });

    db.ActivityLogs.Add(new ActivityLog
    {
      Action = "Trả sách",
      Description = $"Độc giả #{dto.ReaderId} đã trả {borrow.BookTitle}",
      UserEmail = User.Identity?.Name ?? "system",
      Type = "return",
      CreatedAt = returnedAt
    });

    await db.SaveChangesAsync();
    return Ok(new { message = "Đã nhận event trả sách" });
  }

  [HttpPost("book-availability-changed")]
  public async Task<IActionResult> BookAvailabilityChanged(BookAvailabilityChangedDto dto)
  {
    var changedAt = dto.ChangedAt ?? DateTime.UtcNow;
    db.BookAvailabilityEvents.Add(new BookAvailabilityEvent
    {
      BookId = dto.BookId,
      BookTitle = dto.BookTitle,
      Category = dto.Category,
      AvailableCopies = dto.AvailableCopies,
      TotalCopies = dto.TotalCopies,
      ChangedAt = changedAt
    });

    var numericBookId = ParseBookId(dto.BookId);
    if (numericBookId > 0)
    {
      var stat = await db.BookStatistics.FirstOrDefaultAsync(x => x.BookId == numericBookId);
      if (stat == null)
      {
        db.BookStatistics.Add(new BookStatistic { BookId = numericBookId, BookTitle = dto.BookTitle, TotalBorrowed = 0 });
      }
      else
      {
        stat.BookTitle = dto.BookTitle;
        stat.UpdatedAt = DateTime.UtcNow;
      }
    }

    db.ActivityLogs.Add(new ActivityLog
    {
      Action = "Cập nhật sách",
      Description = $"{dto.BookTitle} còn {dto.AvailableCopies}/{dto.TotalCopies} bản",
      UserEmail = User.Identity?.Name ?? "system",
      Type = "book",
      CreatedAt = changedAt
    });

    await db.SaveChangesAsync();
    return Ok(new { message = "Đã nhận event thay đổi tình trạng sách" });
  }

  private static int ParseBookId(string bookId)
  {
    var digits = new string(bookId.Where(char.IsDigit).ToArray());
    return int.TryParse(digits, out var value) ? value : 0;
  }
}
