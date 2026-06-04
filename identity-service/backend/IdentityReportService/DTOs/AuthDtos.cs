namespace IdentityReportService.DTOs;

public record RegisterDto(string FullName, string Email, string Password, string Role = "Reader");
public record LoginDto(string Email, string Password);
public record AuthUserDto(int Id, string FullName, string Email, string Role);
public record AuthResponseDto(string Token, AuthUserDto User);
public record UserDto(int Id, string FullName, string Email, string Role, bool IsActive);
public record CreateUserDto(string FullName, string Email, string Password, string Role = "Reader", bool IsActive = true);
public record UpdateUserDto(string FullName, string Email, string Role = "Reader", bool IsActive = true, string? Password = null);
public record CreateLibraryCardDto(int UserId);
public record LibraryCardDto(int Id, int UserId, string ReaderName, string CardNumber, string Status, DateTime IssuedAt, DateTime ExpiredAt);
public record ValidateCardResponse(bool Valid, string CardStatus, string Message);
public record BorrowedEventDto(int ReaderId, int BookId, string BookTitle, DateTime? BorrowedAt);
public record ReturnedEventDto(int ReaderId, int BookId, DateTime? ReturnedAt, decimal FineAmount);
public record BookAvailabilityChangedDto(string BookId, string BookTitle, string Category, int AvailableCopies, int TotalCopies, DateTime? ChangedAt);
