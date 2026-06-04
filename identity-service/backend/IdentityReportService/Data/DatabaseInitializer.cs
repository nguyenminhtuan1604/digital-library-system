using IdentityReportService.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityReportService.Data;

public static class DatabaseInitializer
{
  public static async Task InitializeAsync(AppDbContext db)
  {
    await db.Database.EnsureCreatedAsync();
    await EnsureExtraTablesAsync(db);
    await SeedAsync(db);
  }

  private static async Task EnsureExtraTablesAsync(AppDbContext db)
  {
    await db.Database.ExecuteSqlRawAsync("""
      IF OBJECT_ID(N'[ReturnEvents]', N'U') IS NULL
      CREATE TABLE [ReturnEvents] (
        [Id] int NOT NULL IDENTITY,
        [ReaderId] int NOT NULL,
        [BookId] int NOT NULL,
        [BookTitle] nvarchar(200) NOT NULL,
        [ReturnedAt] datetime2 NOT NULL,
        [FineAmount] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_ReturnEvents] PRIMARY KEY ([Id])
      );
      """);

    await db.Database.ExecuteSqlRawAsync("""
      IF OBJECT_ID(N'[BookAvailabilityEvents]', N'U') IS NULL
      CREATE TABLE [BookAvailabilityEvents] (
        [Id] int NOT NULL IDENTITY,
        [BookId] nvarchar(50) NOT NULL,
        [BookTitle] nvarchar(200) NOT NULL,
        [Category] nvarchar(120) NOT NULL,
        [AvailableCopies] int NOT NULL,
        [TotalCopies] int NOT NULL,
        [ChangedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_BookAvailabilityEvents] PRIMARY KEY ([Id])
      );
      """);

    await db.Database.ExecuteSqlRawAsync("""
      IF OBJECT_ID(N'[ActivityLogs]', N'U') IS NULL
      CREATE TABLE [ActivityLogs] (
        [Id] int NOT NULL IDENTITY,
        [Action] nvarchar(120) NOT NULL,
        [Description] nvarchar(500) NOT NULL,
        [UserEmail] nvarchar(160) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [Type] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_ActivityLogs] PRIMARY KEY ([Id])
      );
      """);

    await db.Database.ExecuteSqlRawAsync("""
      IF OBJECT_ID(N'[SystemSettings]', N'U') IS NULL
      CREATE TABLE [SystemSettings] (
        [Id] int NOT NULL IDENTITY,
        [SystemName] nvarchar(160) NOT NULL,
        [Language] nvarchar(40) NOT NULL,
        [TimeZone] nvarchar(80) NOT NULL,
        [EnableRealtimeNotification] bit NOT NULL,
        [ThemeMode] nvarchar(30) NOT NULL,
        [PrimaryColor] nvarchar(30) NOT NULL,
        [BorderRadius] int NOT NULL,
        [CollapseSidebar] bit NOT NULL,
        [RequireAdminVerification] bit NOT NULL,
        [EnableActivityLog] bit NOT NULL,
        [AutoLogoutMinutes] int NOT NULL,
        [NotifyExpiringCards] bit NOT NULL,
        [NotifyOverdueBooks] bit NOT NULL,
        [NotifyNewUsers] bit NOT NULL,
        [NotifyByEmail] bit NOT NULL,
        [UpdatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_SystemSettings] PRIMARY KEY ([Id])
      );
      """);
  }

  private static async Task SeedAsync(AppDbContext db)
  {
    await SeedUsersAsync(db);
    await SeedCardsAsync(db);
    await SeedBorrowEventsAsync(db);
    await SeedReturnEventsAsync(db);
    await SeedBookAvailabilityAsync(db);
    await SeedActivityLogsAsync(db);
    await SeedSettingsAsync(db);
    await db.SaveChangesAsync();
  }

  private static async Task SeedUsersAsync(AppDbContext db)
  {
    var users = new[]
    {
      new User { FullName = "Admin", Email = "admin@library.local", PasswordHash = AppDbContext.Hash("Admin@123"), Role = "Admin", IsActive = true, CreatedAt = new DateTime(2026, 6, 1) },
      new User { FullName = "Librarian", Email = "librarian@library.local", PasswordHash = AppDbContext.Hash("Librarian@123"), Role = "Librarian", IsActive = true, CreatedAt = new DateTime(2026, 6, 1) },
      new User { FullName = "Nguyen Van A", Email = "reader@library.local", PasswordHash = AppDbContext.Hash("Reader@123"), Role = "Reader", IsActive = true, CreatedAt = new DateTime(2026, 6, 1) },
      new User { FullName = "Tran Thi B", Email = "reader2@library.local", PasswordHash = AppDbContext.Hash("Reader@123"), Role = "Reader", IsActive = true, CreatedAt = new DateTime(2026, 6, 2) },
      new User { FullName = "Le Van C", Email = "reader3@library.local", PasswordHash = AppDbContext.Hash("Reader@123"), Role = "Reader", IsActive = true, CreatedAt = new DateTime(2026, 6, 2) },
      new User { FullName = "Pham Thi D", Email = "reader4@library.local", PasswordHash = AppDbContext.Hash("Reader@123"), Role = "Reader", IsActive = true, CreatedAt = new DateTime(2026, 6, 3) },
      new User { FullName = "Hoang Van E", Email = "reader5@library.local", PasswordHash = AppDbContext.Hash("Reader@123"), Role = "Reader", IsActive = true, CreatedAt = new DateTime(2026, 6, 3) },
      new User { FullName = "Do Thi F", Email = "reader6@library.local", PasswordHash = AppDbContext.Hash("Reader@123"), Role = "Reader", IsActive = true, CreatedAt = new DateTime(2026, 6, 4) }
    };

    foreach (var user in users)
    {
      if (!await db.Users.AnyAsync(x => x.Email == user.Email))
      {
        db.Users.Add(user);
      }
    }

    await db.SaveChangesAsync();
  }

  private static async Task SeedCardsAsync(AppDbContext db)
  {
    var readers = await db.Users.Where(x => x.Role == "Reader").OrderBy(x => x.Id).Take(8).ToListAsync();
    var index = 1;
    foreach (var reader in readers)
    {
      if (await db.LibraryCards.AnyAsync(x => x.UserId == reader.Id)) continue;
      db.LibraryCards.Add(new LibraryCard
      {
        UserId = reader.Id,
        CardNumber = $"CARD-{reader.Id:0000}",
        Status = index == 5 ? "Locked" : index == 6 ? "Expired" : "Active",
        IssuedAt = new DateTime(2026, 1, Math.Min(index, 28)),
        ExpiredAt = index == 6 ? new DateTime(2026, 1, 6) : new DateTime(2027, 1, Math.Min(index, 28))
      });
      index++;
    }
  }

  private static async Task SeedBorrowEventsAsync(AppDbContext db)
  {
    if (await db.BorrowEvents.CountAsync() >= 10) return;

    var samples = new[]
    {
      new BorrowEvent { ReaderId = 3, BookId = 101, BookTitle = "Clean Code", BorrowedAt = new DateTime(2026, 1, 8), IsReturned = true, ReturnedAt = new DateTime(2026, 1, 15), FineAmount = 0 },
      new BorrowEvent { ReaderId = 3, BookId = 101, BookTitle = "Clean Code", BorrowedAt = new DateTime(2026, 2, 4), IsReturned = true, ReturnedAt = new DateTime(2026, 2, 18), FineAmount = 10000 },
      new BorrowEvent { ReaderId = 4, BookId = 102, BookTitle = "Design Patterns", BorrowedAt = new DateTime(2026, 2, 12), IsReturned = true, ReturnedAt = new DateTime(2026, 2, 20), FineAmount = 0 },
      new BorrowEvent { ReaderId = 5, BookId = 103, BookTitle = "The Pragmatic Programmer", BorrowedAt = new DateTime(2026, 3, 5), IsReturned = true, ReturnedAt = new DateTime(2026, 3, 28), FineAmount = 50000 },
      new BorrowEvent { ReaderId = 6, BookId = 104, BookTitle = "Refactoring", BorrowedAt = new DateTime(2026, 3, 14), IsReturned = true, ReturnedAt = new DateTime(2026, 3, 24), FineAmount = 0 },
      new BorrowEvent { ReaderId = 7, BookId = 105, BookTitle = "Domain-Driven Design", BorrowedAt = new DateTime(2026, 4, 9), IsReturned = true, ReturnedAt = new DateTime(2026, 4, 29), FineAmount = 30000 },
      new BorrowEvent { ReaderId = 8, BookId = 101, BookTitle = "Clean Code", BorrowedAt = new DateTime(2026, 4, 20), IsReturned = true, ReturnedAt = new DateTime(2026, 4, 30), FineAmount = 0 },
      new BorrowEvent { ReaderId = 4, BookId = 106, BookTitle = "Database Internals", BorrowedAt = new DateTime(2026, 5, 3), IsReturned = true, ReturnedAt = new DateTime(2026, 5, 26), FineAmount = 60000 },
      new BorrowEvent { ReaderId = 5, BookId = 102, BookTitle = "Design Patterns", BorrowedAt = DateTime.UtcNow.AddDays(-30), IsReturned = false, FineAmount = 0 },
      new BorrowEvent { ReaderId = 6, BookId = 101, BookTitle = "Clean Code", BorrowedAt = DateTime.UtcNow.AddDays(-25), IsReturned = false, FineAmount = 0 },
      new BorrowEvent { ReaderId = 7, BookId = 107, BookTitle = "System Design Interview", BorrowedAt = DateTime.UtcNow.AddDays(-9), IsReturned = false, FineAmount = 0 }
    };

    db.BorrowEvents.AddRange(samples);
    await db.SaveChangesAsync();

    var grouped = samples.GroupBy(x => new { x.BookId, x.BookTitle });
    foreach (var book in grouped)
    {
      var stat = await db.BookStatistics.FirstOrDefaultAsync(x => x.BookId == book.Key.BookId);
      if (stat == null)
      {
        db.BookStatistics.Add(new BookStatistic { BookId = book.Key.BookId, BookTitle = book.Key.BookTitle, TotalBorrowed = book.Count() });
      }
      else
      {
        stat.TotalBorrowed = Math.Max(stat.TotalBorrowed, book.Count());
        stat.UpdatedAt = DateTime.UtcNow;
      }
    }
  }

  private static async Task SeedReturnEventsAsync(AppDbContext db)
  {
    if (await db.ReturnEvents.CountAsync() >= 8) return;

    var returned = await db.BorrowEvents
      .Where(x => x.IsReturned && x.ReturnedAt != null)
      .OrderBy(x => x.ReturnedAt)
      .Take(8)
      .ToListAsync();

    db.ReturnEvents.AddRange(returned.Select(x => new ReturnEvent
    {
      ReaderId = x.ReaderId,
      BookId = x.BookId,
      BookTitle = x.BookTitle,
      ReturnedAt = x.ReturnedAt ?? DateTime.UtcNow,
      FineAmount = x.FineAmount
    }));
  }

  private static async Task SeedBookAvailabilityAsync(AppDbContext db)
  {
    if (await db.BookAvailabilityEvents.CountAsync() >= 5) return;

    db.BookAvailabilityEvents.AddRange(
      new BookAvailabilityEvent { BookId = "BK-101", BookTitle = "Clean Code", Category = "Cong nghe", AvailableCopies = 3, TotalCopies = 5, ChangedAt = new DateTime(2026, 6, 4, 8, 0, 0) },
      new BookAvailabilityEvent { BookId = "BK-102", BookTitle = "Design Patterns", Category = "Cong nghe", AvailableCopies = 2, TotalCopies = 4, ChangedAt = new DateTime(2026, 6, 4, 9, 0, 0) },
      new BookAvailabilityEvent { BookId = "BK-103", BookTitle = "The Pragmatic Programmer", Category = "Lap trinh", AvailableCopies = 4, TotalCopies = 6, ChangedAt = new DateTime(2026, 6, 4, 10, 0, 0) },
      new BookAvailabilityEvent { BookId = "BK-104", BookTitle = "Refactoring", Category = "Lap trinh", AvailableCopies = 1, TotalCopies = 3, ChangedAt = new DateTime(2026, 6, 4, 11, 0, 0) },
      new BookAvailabilityEvent { BookId = "BK-105", BookTitle = "Domain-Driven Design", Category = "Kien truc", AvailableCopies = 2, TotalCopies = 5, ChangedAt = new DateTime(2026, 6, 4, 12, 0, 0) }
    );
  }

  private static async Task SeedActivityLogsAsync(AppDbContext db)
  {
    if (await db.ActivityLogs.CountAsync() >= 8) return;

    db.ActivityLogs.AddRange(
      new ActivityLog { Action = "Tao doc gia", Description = "Nguyen Van A da duoc tao tai khoan", UserEmail = "admin@library.local", Type = "user", CreatedAt = DateTime.UtcNow.AddHours(-8) },
      new ActivityLog { Action = "Cap the", Description = "Cap the thu vien CARD-0003", UserEmail = "librarian@library.local", Type = "card", CreatedAt = DateTime.UtcNow.AddHours(-7) },
      new ActivityLog { Action = "Muon sach", Description = "Nguyen Van A da muon Clean Code", UserEmail = "reader@library.local", Type = "borrow", CreatedAt = DateTime.UtcNow.AddHours(-6) },
      new ActivityLog { Action = "Tra sach", Description = "Tran Thi B da tra Design Patterns", UserEmail = "reader2@library.local", Type = "return", CreatedAt = DateTime.UtcNow.AddHours(-5) },
      new ActivityLog { Action = "Qua han", Description = "Le Van C co sach dang qua han", UserEmail = "librarian@library.local", Type = "warning", CreatedAt = DateTime.UtcNow.AddHours(-4) },
      new ActivityLog { Action = "Gia han the", Description = "Gia han the thu vien CARD-0004", UserEmail = "librarian@library.local", Type = "card", CreatedAt = DateTime.UtcNow.AddHours(-3) },
      new ActivityLog { Action = "Cap nhat bao cao", Description = "Bao cao hom nay da duoc cap nhat", UserEmail = "admin@library.local", Type = "report", CreatedAt = DateTime.UtcNow.AddHours(-2) },
      new ActivityLog { Action = "Dang ky moi", Description = "Co nguoi dung moi dang ky", UserEmail = "reader6@library.local", Type = "user", CreatedAt = DateTime.UtcNow.AddHours(-1) }
    );
  }

  private static async Task SeedSettingsAsync(AppDbContext db)
  {
    var settings = await db.SystemSettings.OrderBy(x => x.Id).FirstOrDefaultAsync();
    if (settings == null)
    {
      db.SystemSettings.Add(new SystemSettings());
      return;
    }

    if (LooksCorrupted(settings.SystemName) || string.IsNullOrWhiteSpace(settings.SystemName))
    {
      settings.SystemName = "Thư viện số";
    }
    if (LooksCorrupted(settings.Language) || string.IsNullOrWhiteSpace(settings.Language))
    {
      settings.Language = "Tiếng Việt";
    }
    if (string.IsNullOrWhiteSpace(settings.TimeZone) || settings.TimeZone == "Asia/Saigon")
    {
      settings.TimeZone = "Asia/Ho_Chi_Minh";
    }
    if (string.IsNullOrWhiteSpace(settings.ThemeMode) || settings.ThemeMode == "light")
    {
      settings.ThemeMode = "Sáng";
    }
    if (string.IsNullOrWhiteSpace(settings.PrimaryColor) || settings.PrimaryColor.StartsWith("#"))
    {
      settings.PrimaryColor = "Xanh pastel";
    }
    settings.UpdatedAt = DateTime.UtcNow;
  }

  private static bool LooksCorrupted(string? value) =>
    string.IsNullOrEmpty(value) || value.Contains('?') || value.Contains('Æ') || value.Contains('á') || value.Contains('»') || value.Contains('Ã');
}
