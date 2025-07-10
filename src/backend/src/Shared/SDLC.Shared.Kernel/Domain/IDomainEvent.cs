using MediatR;

namespace SDLC.Shared.Kernel.Domain;

public interface IDomainEvent : INotification
{
    DateTime OccurredOn { get; }
} 