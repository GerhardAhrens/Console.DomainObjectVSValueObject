namespace DDD_Demo
{
    using DDDFW;

    public sealed record Address
    {
        public string Street { get; }

        public string ZipCode { get; }

        public string City { get; }

        private Address(string street, string zipCode, string city)
        {
            Street = street;
            ZipCode = zipCode;
            City = city;
        }

        public static Result<Address> Create(string street, string zipCode, string city)
        {
            Guard.NotNullOrWhiteSpace(street, nameof(street));
            Guard.NotNullOrWhiteSpace(zipCode, nameof(zipCode));
            Guard.NotNullOrWhiteSpace(city, nameof(city));

            return Result<Address>.Ok(new Address(street, zipCode,city));
        }

        public override string ToString() => $"{Street}, {ZipCode} {City}";
    }
}
