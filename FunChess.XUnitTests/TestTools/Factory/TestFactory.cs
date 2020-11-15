using FunChess.XUnitTests.TestTools.Helpers;

namespace FunChess.XUnitTests.TestTools.Factory
{
    public class TestFactory
    {
        public TestFactory() { }

        public TestUtils CreateTestHelper()
        {
            return new TestUtils();
        }
    }
}
