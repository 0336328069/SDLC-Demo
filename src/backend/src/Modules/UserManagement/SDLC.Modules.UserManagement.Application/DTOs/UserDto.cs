namespace SDLC.Modules.UserManagement.Application.DTOs;

public record UserDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Role,
    bool IsActive,
    DateTime? LastLoginAt,
    DateTime CreatedAt
)
{
    public string FullName => $"{FirstName} {LastName}";
}

public record CreateUserDto(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string Role
);

public record UpdateUserProfileDto(
    string FirstName,
    string LastName
);

public record PagedResult<T>(
    IReadOnlyList<T> Items,
    int TotalCount,
    int PageNumber,
    int PageSize
)
{
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
} 