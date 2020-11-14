namespace FunChess.Core.Factory
{
    public static class Beyond
    {
        private static CoreFactory core;

        public static CoreFactory Core
        {
            get
            {
                if (core == null)
                {
                    core = new CoreFactory();
                    return core;
                }

                return core;
            }
        }
    }
}
