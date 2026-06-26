namespace DDD_Demo
{
    using DDDFW;

    public sealed record Email
    {
        public string Value { get; }

        public Email(string value)
        {
            Guard.NotNullOrWhiteSpace(value, nameof(value));

            Value = value.Trim().ToLowerInvariant();
        }

        public override string ToString() => Value;
    }
}
