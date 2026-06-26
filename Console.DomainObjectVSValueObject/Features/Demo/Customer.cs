namespace DDD_Demo
{

    using DDDFW;

    public sealed class Customer : AggregateRoot<EntityId<Customer>>, IAuditable, ISoftDelete
    {
        public PersonName Name { get; private set; }

        public Email Email { get; private set; }

        public Address Address { get; private set; }

        public bool Active { get; private set; }

        // IAuditable

        public DateTime CreatedOn { get; internal set; }

        public DateTime? ModifiedOn { get; internal set; }

        // ISoftDelete

        public bool IsDeleted { get; private set; }

        public DateTime? DeletedOn { get; private set; }

        private Customer(EntityId<Customer> id, PersonName name, Email email, Address address) : base(id)
        {
            Name = name;
            Email = email;
            Address = address;

            Active = true;
        }

        public static Result<Customer> Create(PersonName name, Email email, Address address)
        {
            var customer = new Customer(EntityId<Customer>.New(), name, email, address);

            customer.Raise(new CustomerCreated(customer.Id));

            return Result<Customer>.Ok(customer);
        }

        public Result Rename(PersonName name)
        {
            if (Name == name)
                return Result.Ok();

            Name = name;

            Raise(new CustomerRenamed(Id, name));

            return Result.Ok();
        }

        public Result ChangeEmail(Email email)
        {
            if (Email == email)
                return Result.Ok();

            Email = email;

            Raise(new CustomerEmailChanged(Id, email));

            return Result.Ok();
        }

        public Result Move(Address address)
        {
            if (Address == address)
                return Result.Ok();

            Address = address;

            Raise(new CustomerMoved(Id, address));

            return Result.Ok();
        }

        public Result Delete()
        {
            if (IsDeleted)
                return Result.Fail("Customer wurde bereits gelöscht.");

            IsDeleted = true;
            DeletedOn = DateTime.UtcNow;

            Raise(new CustomerDeleted(Id));

            return Result.Ok();
        }
    }
}
