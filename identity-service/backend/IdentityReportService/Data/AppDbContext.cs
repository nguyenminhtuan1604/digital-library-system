using IdentityReportService.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace IdentityReportService.Data;
public class AppDbContext : DbContext {
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
  public DbSet<User> Users => Set<User>();
  public DbSet<LibraryCard> LibraryCards => Set<LibraryCard>();
  public DbSet<BorrowEvent> BorrowEvents => Set<BorrowEvent>();
  public DbSet<ReturnEvent> ReturnEvents => Set<ReturnEvent>();
  public DbSet<BookStatistic> BookStatistics => Set<BookStatistic>();
  public DbSet<BookAvailabilityEvent> BookAvailabilityEvents => Set<BookAvailabilityEvent>();
  public DbSet<ActivityLog> ActivityLogs => Set<ActivityLog>();
  public DbSet<SystemSettings> SystemSettings => Set<SystemSettings>();
  protected override void OnModelCreating(ModelBuilder b) {
    b.Entity<User>().HasIndex(x => x.Email).IsUnique();
    b.Entity<LibraryCard>().HasIndex(x => x.CardNumber).IsUnique();
    b.Entity<BorrowEvent>().Property(x => x.FineAmount).HasColumnType("decimal(18,2)");
    b.Entity<ReturnEvent>().Property(x => x.FineAmount).HasColumnType("decimal(18,2)");
    b.Entity<User>().HasData(
      new User { Id=1, FullName="Admin", Email="admin@library.local", PasswordHash=Hash("Admin@123"), Role="Admin", IsActive=true, CreatedAt=new DateTime(2026,6,1) },
      new User { Id=2, FullName="Librarian", Email="librarian@library.local", PasswordHash=Hash("Librarian@123"), Role="Librarian", IsActive=true, CreatedAt=new DateTime(2026,6,1) },
      new User { Id=3, FullName="Nguyen Van A", Email="reader@library.local", PasswordHash=Hash("Reader@123"), Role="Reader", IsActive=true, CreatedAt=new DateTime(2026,6,1) },
      new User { Id=4, FullName="Tran Thi B", Email="reader2@library.local", PasswordHash=Hash("Reader@123"), Role="Reader", IsActive=true, CreatedAt=new DateTime(2026,6,2) },
      new User { Id=5, FullName="Le Van C", Email="reader3@library.local", PasswordHash=Hash("Reader@123"), Role="Reader", IsActive=true, CreatedAt=new DateTime(2026,6,2) },
      new User { Id=6, FullName="Pham Thi D", Email="reader4@library.local", PasswordHash=Hash("Reader@123"), Role="Reader", IsActive=true, CreatedAt=new DateTime(2026,6,3) },
      new User { Id=7, FullName="Hoang Van E", Email="reader5@library.local", PasswordHash=Hash("Reader@123"), Role="Reader", IsActive=true, CreatedAt=new DateTime(2026,6,3) },
      new User { Id=8, FullName="Do Thi F", Email="reader6@library.local", PasswordHash=Hash("Reader@123"), Role="Reader", IsActive=true, CreatedAt=new DateTime(2026,6,4) }
    );
    b.Entity<LibraryCard>().HasData(
      new LibraryCard { Id=1, UserId=3, CardNumber="CARD-0003", Status="Active", IssuedAt=new DateTime(2026,1,1), ExpiredAt=new DateTime(2027,1,1)},
      new LibraryCard { Id=2, UserId=4, CardNumber="CARD-0004", Status="Active", IssuedAt=new DateTime(2026,1,2), ExpiredAt=new DateTime(2027,1,2)},
      new LibraryCard { Id=3, UserId=5, CardNumber="CARD-0005", Status="Active", IssuedAt=new DateTime(2026,1,3), ExpiredAt=new DateTime(2027,1,3)},
      new LibraryCard { Id=4, UserId=6, CardNumber="CARD-0006", Status="Active", IssuedAt=new DateTime(2026,1,4), ExpiredAt=new DateTime(2027,1,4)},
      new LibraryCard { Id=5, UserId=7, CardNumber="CARD-0007", Status="Locked", IssuedAt=new DateTime(2026,1,5), ExpiredAt=new DateTime(2027,1,5)},
      new LibraryCard { Id=6, UserId=8, CardNumber="CARD-0008", Status="Expired", IssuedAt=new DateTime(2025,1,6), ExpiredAt=new DateTime(2026,1,6)}
    );
  }
  public static string Hash(string input) => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(input)));
}
