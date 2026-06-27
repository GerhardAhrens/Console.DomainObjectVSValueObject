namespace DDD_Demo
{

    using DDDFW;

    public sealed class Customer : AuditableAggregateRoot<EntityId<Customer>>, IAuditable, ISoftDelete
    {
        public PersonName Name { get; private set; }

        public Email Email { get; private set; }

        public Address Address { get; private set; }

        public bool Active { get; private set; }

        // ISoftDelete
        public bool IsDeleted { get; private set; }

        public DateTime? DeletedOn { get; private set; }
        public string DeletedFrom { get; private set; }
        private Customer(EntityId<Customer> id, PersonName name, Email email, Address address) : base(id)
        {
            Name = name;
            Email = email;
            Address = address;

            Active = true;
        }

        public static Result<Customer> Create(PersonName name, Email email, Address address)
        {
            var customer = new Customer(EntityId.New<Customer>(), name, email, address);

            customer.Raise(new CustomerCreated(customer.Id));
            customer.CreatedOn = DateTime.UtcNow;
            customer.CreatedFrom = Environment.UserName;
            return Result<Customer>.Ok(customer);
        }

        public Result Rename(string firstName, string lastName)
        {
            var nameResult = PersonName.Create(firstName, lastName);

            if (nameResult.Success == false)
            {
                return Result.Fail(nameResult.Errors);
            }

            if (Name == nameResult.Value)
            {
                return Result.Ok();
            }

            Name = nameResult.Value!;
            base.ModifiedOn = DateTime.UtcNow;
            base.ModifiedFrom = Environment.UserName;
            Raise(new CustomerRenamed(Id, Name));

            return Result.Ok();
        }

        public Result ChangeEmail(string email)
        {
            var emailResult = Email.Create(email);
            if (emailResult.Success == false)
            {
                return Result.Fail(emailResult.Errors);
            }

            if (Email == emailResult.Value)
            {
                return Result.Ok();
            }

            Email = emailResult.Value;

            base.ModifiedOn = DateTime.UtcNow;
            base.ModifiedFrom = Environment.UserName;

            Raise(new CustomerEmailChanged(Id, Email));

            return Result.Ok();
        }

        public Result Move(string street, string zipCode, string city)
        {
            var addressResult = Address.Create(street, zipCode, city);
            if (addressResult.Success == false)
            {
                return Result.Fail(addressResult.Errors);
            }

            Address = addressResult.Value;

            base.ModifiedOn = DateTime.UtcNow;
            base.ModifiedFrom = Environment.UserName;

            Raise(new CustomerMoved(Id, Address));

            return Result.Ok();
        }

        public Result Delete()
        {
            if (IsDeleted == true)
            {
                return Result.Fail(CustomerErrors.AlreadyDeleted);
            }

            IsDeleted = true;

            this.DeletedOn = DateTime.UtcNow;
            this.DeletedFrom = Environment.UserName;

            Raise(new CustomerDeleted(Id));

            return Result.Ok();
        }
    }
}
