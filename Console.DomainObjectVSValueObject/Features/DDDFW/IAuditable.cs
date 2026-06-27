namespace DDDFW
{
    public interface IAuditable
    {
        DateTime CreatedOn { get; }
        String CreatedFrom { get; }

        DateTime? ModifiedOn { get; }
        string ModifiedFrom { get; }
    }
}
