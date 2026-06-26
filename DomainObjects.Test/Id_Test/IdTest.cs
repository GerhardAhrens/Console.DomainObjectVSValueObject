namespace DomainDrivenDesign.DomainObjects.Test.IdTests
{
    using System.Globalization;
    using System.Runtime.Versioning;

    using DomainObject;

    [TestClass]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Plattformkompatibilität überprüfen", Justification = "<Ausstehend>")]
    public class IdTest
    {
        [TestInitialize]
        public void Initialize()
        {
            CultureInfo culture = new CultureInfo("de-DE");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        [TestMethod]
        public void WhenEmptyGuid_ShouldThrowException()
        {
            try
            {
                Action act = () => Id<TestableEntity>.Create(Guid.Empty);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.GetType() == typeof(ArgumentException));
            }
        }

        [TestMethod]
        public void WhenCreateNew_ShouldNotBeEmpty()
        { // arrange
            var id = Id<TestableEntity>.New();

            // act
            var actual = id.ToGuid();

            Assert.IsTrue(actual != Guid.Empty);

            // assert
            Assert.AreEqual(typeof(Guid),actual.GetType());
        }

        [TestMethod]
        public void WhenValuePassedToConstructor_ShouldReturnConstructorValueAsString()
        {
            // arrange
            var id = Id<TestableEntity>.Create(Guid.NewGuid());

            // act
            var actual = id.ToString();

            // assert
            Assert.AreNotEqual(string.Empty, actual.ToString());
        }
    }
}
