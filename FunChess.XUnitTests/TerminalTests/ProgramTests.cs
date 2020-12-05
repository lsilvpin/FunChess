using FunChess.Terminal;
using System;
using Xunit;

namespace FunChess.XUnitTests.TerminalTests
{
    public class ProgramTests
    {
        public ProgramTests()
        {
        }


        [Fact]
        public void MainTest()
        {
            // Arrange
            Exception exception = null;

            // Act
            try
            {
                Program.Main(new string[1]);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            // Assert
            Assert.Null(exception);
        }
    }
}
