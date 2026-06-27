namespace DDD_Demo
{
    using DDDFW;

    public static class CustomerErrors
    {
        public static readonly ResultError NameEmpty =
            new(
                "Customer.Name.Empty",
                "Der Name darf nicht leer sein.");

        public static readonly ResultError InvalidEmail =
            new(
                "Customer.Email.Invalid",
                "Die E-Mail-Adresse ist ungültig.");

        public static readonly ResultError AlreadyDeleted =
            new(
                "Customer.AlreadyDeleted",
                "Der Kunde wurde bereits gelöscht.");
    }
}
