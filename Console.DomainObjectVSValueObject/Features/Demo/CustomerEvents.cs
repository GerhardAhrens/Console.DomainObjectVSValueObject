namespace DDD_Demo
{
    using DDDFW;

    public sealed record CustomerCreated(EntityId<Customer> CustomerId) : DomainEvent;

    public sealed record CustomerRenamed(EntityId<Customer> CustomerId, PersonName Name) : DomainEvent;

    public sealed record CustomerEmailChanged(EntityId<Customer> CustomerId, Email Email) : DomainEvent;

    public sealed record CustomerMoved(EntityId<Customer> CustomerId, Address Address) : DomainEvent;

    public sealed record CustomerDeleted(EntityId<Customer> CustomerId) : DomainEvent;
}
