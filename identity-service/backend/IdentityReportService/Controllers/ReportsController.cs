using System.Text;
using IdentityReportService.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityReportService.Controllers;

[ApiController, Route("api/reports"), Authorize(Roles = "Admin,Librarian")]
public class ReportsController(AppDbContext db) : ControllerBase
{
  [HttpGet("overview")]
  public async Task<IActionResult> Overview([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
  {
    var borrowEvents = FilterBorrowEvents(fromDate, toDate);
    var returnEvents = FilterReturnEvents(fromDate, toDate);
    var now = DateTime.UtcNow;

    var topReader = await borrowEvents
      .GroupBy(x => x.ReaderId)
      .Select(g => new { readerId = g.Key, borrowCount = g.Count() })
      .OrderByDescending(x => x.borrowCount)
      .FirstOrDefaultAsync();

    var topReaderName = topReader == null
      ? null
      : await db.Users.Where(x => x.Id == topReader.readerId).Select(x => x.FullName).FirstOrDefaultAsync();

    var topBook = await borrowEvents
      .GroupBy(x => new { x.BookId, x.BookTitle })
      .Select(g => new { bookId = $"BK-{g.Key.BookId}", bookTitle = g.Key.BookTitle, borrowCount = g.Count() })
      .OrderByDescending(x => x.borrowCount)
      .FirstOrDefaultAsync();

    return Ok(new
    {
      totalReaders = await db.Users.AsNoTracking().CountAsync(x => x.Role == "Reader"),
      totalCards = await db.LibraryCards.AsNoTracking().CountAsync(),
      totalBorrowed = await borrowEvents.CountAsync(),
      totalReturned = await returnEvents.CountAsync(),
      overdueBooks = await borrowEvents.CountAsync(x => !x.IsReturned && x.BorrowedAt.AddDays(14) < now),
      totalFines = await returnEvents.SumAsync(x => x.FineAmount),
      topReader = topReader == null ? null : new { topReader.readerId, readerName = topReaderName, topReader.borrowCount },
      topBook
    });
  }

  [HttpGet("books")]
  public async Task<IActionResult> Books([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
  {
    var availability = FilterAvailabilityEvents(fromDate, toDate);
    var availabilityRows = await availability.ToListAsync();
    var latestBooks = availabilityRows
      .GroupBy(x => x.BookId)
      .Select(g => g.OrderByDescending(x => x.ChangedAt).First())
      .ToList();

    var categories = latestBooks
      .GroupBy(x => string.IsNullOrWhiteSpace(x.Category) ? "Khác" : x.Category)
      .Select(g => new { category = g.Key, totalBooks = g.Count(), availableCopies = g.Sum(x => x.AvailableCopies), totalCopies = g.Sum(x => x.TotalCopies) })
      .OrderByDescending(x => x.totalBooks)
      .ToList();

    var topBooks = await FilterBorrowEvents(fromDate, toDate)
      .GroupBy(x => new { x.BookId, x.BookTitle })
      .Select(g => new { bookId = $"BK-{g.Key.BookId}", bookTitle = g.Key.BookTitle, borrowCount = g.Count() })
      .OrderByDescending(x => x.borrowCount)
      .Take(10)
      .ToListAsync();

    return Ok(new
    {
      totalBookTitles = latestBooks.Count,
      availableCopies = latestBooks.Sum(x => x.AvailableCopies),
      totalCopies = latestBooks.Sum(x => x.TotalCopies),
      categories,
      topBooks
    });
  }

  [HttpGet("borrowing")]
  public async Task<IActionResult> Borrowing([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
  {
    var borrows = FilterBorrowEvents(fromDate, toDate);
    var returns = FilterReturnEvents(fromDate, toDate);
    var now = DateTime.UtcNow;

    var borrowRows = await borrows.Select(x => new { date = x.BorrowedAt, borrowed = 1, returned = 0 }).ToListAsync();
    var returnRows = await returns.Select(x => new { date = x.ReturnedAt, borrowed = 0, returned = 1 }).ToListAsync();

    var monthly = borrowRows.Concat(returnRows)
      .GroupBy(x => new { x.date.Year, x.date.Month })
      .Select(g => new
      {
        month = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMM"),
        borrowed = g.Sum(x => x.borrowed),
        returned = g.Sum(x => x.returned)
      })
      .OrderBy(x => DateTime.ParseExact(x.month, "MMM", System.Globalization.CultureInfo.InvariantCulture).Month)
      .ToList();

    return Ok(new
    {
      totalBorrowed = await borrows.CountAsync(),
      totalReturned = await returns.CountAsync(),
      overdueBooks = await borrows.CountAsync(x => !x.IsReturned && x.BorrowedAt.AddDays(14) < now),
      monthly
    });
  }

  [HttpGet("fines")]
  public async Task<IActionResult> Fines([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
  {
    var returns = FilterReturnEvents(fromDate, toDate);
    var rows = await returns
      .Where(x => x.FineAmount > 0)
      .Select(x => new { x.ReaderId, x.BookTitle, x.FineAmount, x.ReturnedAt })
      .ToListAsync();

    var readerNames = await db.Users.AsNoTracking().ToDictionaryAsync(x => x.Id, x => x.FullName);
    var fineReaders = rows.Select(x => new
    {
      x.ReaderId,
      readerName = readerNames.GetValueOrDefault(x.ReaderId, $"Độc giả #{x.ReaderId}"),
      x.BookTitle,
      x.FineAmount,
      x.ReturnedAt
    });

    return Ok(new
    {
      totalFines = rows.Sum(x => x.FineAmount),
      monthly = rows
        .GroupBy(x => new { x.ReturnedAt.Year, x.ReturnedAt.Month })
        .Select(g => new { month = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMM"), amount = g.Sum(x => x.FineAmount) })
        .OrderBy(x => x.month)
        .ToList(),
      fineReaders
    });
  }

  [HttpGet("export")]
  public async Task<IActionResult> Export([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate, [FromQuery] string? format)
  {
    if (!string.IsNullOrWhiteSpace(format) && !format.Equals("csv", StringComparison.OrdinalIgnoreCase))
    {
      return BadRequest(new { message = "Định dạng chưa được hỗ trợ" });
    }

    var borrowEvents = FilterBorrowEvents(fromDate, toDate);
    var returnEvents = FilterReturnEvents(fromDate, toDate);
    var now = DateTime.UtcNow;

    var rows = new[]
    {
      ("Tổng độc giả", await db.Users.AsNoTracking().CountAsync(x => x.Role == "Reader"), ""),
      ("Tổng thẻ thư viện", await db.LibraryCards.AsNoTracking().CountAsync(), ""),
      ("Tổng lượt mượn", await borrowEvents.CountAsync(), ""),
      ("Tổng lượt trả", await returnEvents.CountAsync(), ""),
      ("Sách quá hạn", await borrowEvents.CountAsync(x => !x.IsReturned && x.BorrowedAt.AddDays(14) < now), ""),
      ("Tổng tiền phạt", (int)await returnEvents.SumAsync(x => x.FineAmount), "")
    };

    var csv = new StringBuilder();
    csv.AppendLine("Loại báo cáo,Số lượng,Ghi chú");
    foreach (var row in rows)
    {
      csv.AppendLine($"{Escape(row.Item1)},{row.Item2},{Escape(row.Item3)}");
    }

    return File(Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(csv.ToString())).ToArray(), "text/csv", "report-summary.csv");
  }

  private IQueryable<Models.BorrowEvent> FilterBorrowEvents(DateTime? fromDate, DateTime? toDate)
  {
    var query = db.BorrowEvents.AsNoTracking();
    if (fromDate.HasValue) query = query.Where(x => x.BorrowedAt >= fromDate.Value);
    if (toDate.HasValue) query = query.Where(x => x.BorrowedAt <= toDate.Value.Date.AddDays(1).AddTicks(-1));
    return query;
  }

  private IQueryable<Models.ReturnEvent> FilterReturnEvents(DateTime? fromDate, DateTime? toDate)
  {
    var query = db.ReturnEvents.AsNoTracking();
    if (fromDate.HasValue) query = query.Where(x => x.ReturnedAt >= fromDate.Value);
    if (toDate.HasValue) query = query.Where(x => x.ReturnedAt <= toDate.Value.Date.AddDays(1).AddTicks(-1));
    return query;
  }

  private IQueryable<Models.BookAvailabilityEvent> FilterAvailabilityEvents(DateTime? fromDate, DateTime? toDate)
  {
    var query = db.BookAvailabilityEvents.AsNoTracking();
    if (fromDate.HasValue) query = query.Where(x => x.ChangedAt >= fromDate.Value);
    if (toDate.HasValue) query = query.Where(x => x.ChangedAt <= toDate.Value.Date.AddDays(1).AddTicks(-1));
    return query;
  }

  private static string Escape(string value) =>
    value.Contains(',') || value.Contains('"') || value.Contains('\n')
      ? $"\"{value.Replace("\"", "\"\"")}\""
      : value;
}
