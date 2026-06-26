namespace DDD_Demo
{
    using DDDFW;

    public sealed record PersonName
    {
        public string FirstName { get; }

        public string LastName { get; }

        public PersonName(string firstName, string lastName)
        {
            Guard.NotNullOrWhiteSpace(firstName, nameof(firstName));
            Guard.NotNullOrWhiteSpace(lastName, nameof(lastName));

            FirstName = firstName.Trim();
            LastName = lastName.Trim();
        }

        public override string ToString() => $"{FirstName} {LastName}";
    }
}
