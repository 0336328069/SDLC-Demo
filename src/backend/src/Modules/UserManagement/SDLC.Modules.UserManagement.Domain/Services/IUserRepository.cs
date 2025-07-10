using SDLC.Modules.UserManagement.Domain.Entities;

namespace SDLC.Modules.UserManagement.Domain.Services;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<User>> GetAllAsync(int pageNumber, int pageSize, string? searchTerm = null, bool? isActive = null, CancellationToken cancellationToken = default);
    Task<int> GetTotalCountAsync(string? searchTerm = null, bool? isActive = null, CancellationToken cancellationToken = default);
    Task<User> AddAsync(User user, CancellationToken cancellationToken = default);
    Task UpdateAsync(User user, CancellationToken cancellationToken = default);
    Task DeleteAsync(User user, CancellationToken cancellationToken = default);
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
} 