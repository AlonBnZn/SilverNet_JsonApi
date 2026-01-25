namespace SilverNetJsonApiAssigment.Tests
{
    [TestFixture]
    public abstract class SpecificationBase
    {
        [OneTimeSetUp]
        public void BaseSetUp()
        {
            Given();
            When();
        }

        [OneTimeTearDown]
        public void BaseTearDown()
        {
            Cleanup();
        }

        protected virtual void Given() { }

        protected virtual void When() { }

        protected virtual void Cleanup() { }
    }
}
