namespace DDD_Demo
{
    using System.Text.RegularExpressions;

    using DDDFW;

    public sealed record Email
    {
        private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public string Value { get; }

        private Email(string value)
        {
            Value = value;
        }

        public static Result<Email> Create(string value)
        {
            Guard.NotNullOrWhiteSpace(value, nameof(value));

            if (EmailRegex.IsMatch(value) == false)
            {
                return Result<Email>.Fail(CustomerErrors.InvalidEmail);
            }

            return Result<Email>.Ok(new Email(value));
        }
    }
}
