using SDLC.Shared.Kernel.Domain;
using SDLC.Modules.UserManagement.Domain.Entities;

namespace SDLC.Modules.UserManagement.Domain.Events;

public record UserCreatedEvent(Guid UserId, string Email, string FullName) : BaseDomainEvent;

public record UserProfileUpdatedEvent(Guid UserId, string FullName) : BaseDomainEvent;

public record UserRoleChangedEvent(Guid UserId, UserRole PreviousRole, UserRole NewRole, string ChangedBy) : BaseDomainEvent;

public record UserActivatedEvent(Guid UserId, string ActivatedBy) : BaseDomainEvent;

public record UserDeactivatedEvent(Guid UserId, string DeactivatedBy) : BaseDomainEvent; 