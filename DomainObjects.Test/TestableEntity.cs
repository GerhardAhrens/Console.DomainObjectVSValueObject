namespace DomainDrivenDesign.DomainObjects.Test
{
    using System.Runtime.Versioning;

    using DomainObject;

    [SupportedOSPlatform("windows7.0")]
    public class TestableEntity : Entity<TestableEntity>
    {
        public Name Name { get; }

        public static TestableEntity Create(Id<TestableEntity> id, Name number)
        {
            return new TestableEntity(id, number);
        }
        
        private TestableEntity(Id<TestableEntity> id, Name number) : base(id)
        {
            Name = number;
        }
    }
}
