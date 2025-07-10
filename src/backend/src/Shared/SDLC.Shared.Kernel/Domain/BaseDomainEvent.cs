namespace SDLC.Shared.Kernel.Domain;

public abstract record BaseDomainEvent : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
} 