namespace DDDFW
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; }

        DateTime? DeletedOn { get; }
    }
}
