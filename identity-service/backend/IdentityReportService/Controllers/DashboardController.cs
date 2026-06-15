using IdentityReportService.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityReportService.Controllers;

[ApiController, Route("api/dashboard"), Authorize(Roles = "Admin,Librarian")]
public class DashboardController(AppDbContext db) : ControllerBase
{
  [HttpGet]
  public async Task<IActionResult> Get()
  {
    var now = DateTime.UtcNow;
    var users = db.Users.AsNoTracking();
    var cards = db.LibraryCards.AsNoTracking();
    var borrows = db.BorrowEvents.AsNoTracking();
    var returns = db.ReturnEvents.AsNoTracking();

    var borrowRows = await borrows.Select(x => new { date = x.BorrowedAt, borrowed = 1, returned = 0 }).ToListAsync();
    var returnRows = await returns.Select(x => new { date = x.ReturnedAt, borrowed = 0, returned = 1 }).ToListAsync();
    var monthly = borrowRows.Concat(returnRows)
      .GroupBy(x => new { x.date.Year, x.date.Month })
      .OrderBy(x => x.Key.Year).ThenBy(x => x.Key.Month)
      .Select(g => new
      {
        month = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMM"),
        borrowed = g.Sum(x => x.borrowed),
        returned = g.Sum(x => x.returned)
      })
      .ToList();

    var topBooks = await borrows
      .GroupBy(x => new { x.BookId, x.BookTitle })
      .Select(g => new { bookId = $"BK-{g.Key.BookId}", bookTitle = g.Key.BookTitle, borrowCount = g.Count() })
      .OrderByDescending(x => x.borrowCount)
      .Take(5)
      .ToListAsync();

    var recentActivities = await db.ActivityLogs.AsNoTracking()
      .OrderByDescending(x => x.CreatedAt)
      .Take(8)
      .Select(x => new { x.Action, x.Description, x.CreatedAt, x.Type })
      .ToListAsync();

    return Ok(new
    {
      totalReaders = await users.CountAsync(x => x.Role == "Reader"),
      activeCards = await cards.CountAsync(x => x.Status == "Active" && x.ExpiredAt > now),
      borrowedBooks = await borrows.CountAsync(x => !x.IsReturned),
      overdueBooks = await borrows.CountAsync(x => !x.IsReturned && x.BorrowedAt.AddDays(14) < now),
      totalFines = await returns.SumAsync(x => x.FineAmount),
      monthlyBorrowing = monthly,
      cardStatus = new[]
      {
        new { status = "Active", count = await cards.CountAsync(x => x.Status == "Active" && x.ExpiredAt > now) },
        new { status = "Locked", count = await cards.CountAsync(x => x.Status == "Locked") },
        new { status = "Expired", count = await cards.CountAsync(x => x.Status == "Expired" || x.ExpiredAt <= now) }
      },
      topBooks,
      recentActivities
    });
  }
}
