namespace DDDFW
{
    public interface IAuditable
    {
        DateTime CreatedOn { get; }

        DateTime? ModifiedOn { get; }
    }
}
