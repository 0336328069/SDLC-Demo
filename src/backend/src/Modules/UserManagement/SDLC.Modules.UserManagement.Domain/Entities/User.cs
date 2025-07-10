using SDLC.Shared.Kernel.Domain;
using SDLC.Shared.Kernel.ValueObjects;
using SDLC.Modules.UserManagement.Domain.Events;

namespace SDLC.Modules.UserManagement.Domain.Entities;

public class User : AggregateRoot<Guid>
{
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public Email Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = string.Empty;
    public UserRole Role { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime? LastLoginAt { get; private set; }

    private User() : base() { }

    public User(string firstName, string lastName, Email email, string passwordHash, UserRole role)
        : base(Guid.NewGuid())
    {
        FirstName = Guard.Against.NullOrEmpty(firstName, nameof(firstName));
        LastName = Guard.Against.NullOrEmpty(lastName, nameof(lastName));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        PasswordHash = Guard.Against.NullOrEmpty(passwordHash, nameof(passwordHash));
        Role = role;
        IsActive = true;

        AddDomainEvent(new UserCreatedEvent(Id, Email.Value, $"{FirstName} {LastName}"));
    }

    public void UpdateProfile(string firstName, string lastName)
    {
        FirstName = Guard.Against.NullOrEmpty(firstName, nameof(firstName));
        LastName = Guard.Against.NullOrEmpty(lastName, nameof(lastName));

        AddDomainEvent(new UserProfileUpdatedEvent(Id, $"{FirstName} {LastName}"));
    }

    public void ChangeRole(UserRole newRole, string changedBy)
    {
        if (Role == newRole) return;

        var previousRole = Role;
        Role = newRole;
        UpdateTimestamp(changedBy);

        AddDomainEvent(new UserRoleChangedEvent(Id, previousRole, newRole, changedBy));
    }

    public void Activate(string activatedBy)
    {
        if (IsActive) return;

        IsActive = true;
        UpdateTimestamp(activatedBy);

        AddDomainEvent(new UserActivatedEvent(Id, activatedBy));
    }

    public void Deactivate(string deactivatedBy)
    {
        if (!IsActive) return;

        IsActive = false;
        UpdateTimestamp(deactivatedBy);

        AddDomainEvent(new UserDeactivatedEvent(Id, deactivatedBy));
    }

    public void RecordLogin()
    {
        LastLoginAt = DateTime.UtcNow;
    }

    public string FullName => $"{FirstName} {LastName}";
}

public enum UserRole
{
    Admin = 1,
    ProjectManager = 2,
    Developer = 3,
    Tester = 4,
    Viewer = 5
} 