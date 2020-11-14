using FunChess.Core.BusinessLogic;
using FunChess.Core.Factory;
using FunChess.Core.Models;
using Xunit;

namespace FunChess.XUnitTests.CoreTests.BusinessLogicTests
{
    public class BrainTests
    {
        private readonly CoreFactory core;
        private readonly Brain brain;

        public BrainTests()
        {
            core = Beyond.Core;
            brain = core.CreateBrain();
        }

        [Theory]
        [InlineData("b2")]
        [InlineData("e5")]
        [InlineData("f7")]
        [InlineData("z2")]
        [InlineData("a9")]
        public void IsSquareNamesBeenConvertedToCoordinatesCorrectly(string squareName)
        {
            Position coordinate = brain.ConvertSquareNameToCoordinate(squareName);

            if (squareName == "b2")
                Assert.Equal(core.CreatePosition(1, 1), coordinate);
            else if (squareName == "e5")
                Assert.Equal(core.CreatePosition(4, 4), coordinate);
            else if (squareName == "f7")
                Assert.Equal(core.CreatePosition(5, 6), coordinate);
            else
                Assert.Null(coordinate);
        }
    }
}
