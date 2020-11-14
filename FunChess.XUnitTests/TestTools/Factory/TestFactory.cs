using FunChess.XUnitTests.TestTools.Helpers;

namespace FunChess.XUnitTests.TestTools.Factory
{
    public class TestFactory
    {
        public TestFactory() { }

        public TestHelper CreateTestHelper()
        {
            return new TestHelper();
        }
    }
}
