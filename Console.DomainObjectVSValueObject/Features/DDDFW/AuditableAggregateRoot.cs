namespace DDDFW
{
    public abstract class AuditableAggregateRoot<TId> : AggregateRoot<TId>, IAuditable
    {
        public DateTime CreatedOn { get; internal set; }
        public string CreatedFrom { get; internal set; }

        public DateTime? ModifiedOn { get; internal set; }
        public string ModifiedFrom { get; internal set; }

        protected AuditableAggregateRoot(TId id) : base(id)
        {
        }
    }
}
