namespace FunChess.XUnitTests.TestTools.Factory
{
    public static class TestVortex
    {
        private static TestFactory test;

        public static TestFactory Test
        {
            get
            {
                if (test == null)
                    test = new TestFactory();

                return test;
            }
        }
    }
}
