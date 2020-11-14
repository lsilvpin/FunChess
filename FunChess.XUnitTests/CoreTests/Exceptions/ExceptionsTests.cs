using FunChess.Core.Enums;
using FunChess.Core.Exceptions;
using FunChess.Core.Factory;
using FunChess.Core.Models;
using FunChess.Core.Models.Pieces;
using Xunit;

namespace FunChess.XUnitTests.CoreTests.Exceptions
{
    public class ExceptionsTests
    {
        private readonly CoreFactory core;

        public ExceptionsTests()
        {
            core = Beyond.Core;
        }

        [Fact]
        public void IsPiecesOutOfBoardExceptionWorkingCorrectly()
        {
            // Arrange
            Bishop bishop = core.CreateBishop(PieceColor.White);
            Position position = core.CreatePosition(-1, 18);
            bishop.Position = position;

            // Act
            PieceOutOfBoardException outOfBoardException = Assert.Throws<PieceOutOfBoardException>(bishop.ValidateIfPositionIsInsideLimits);

            // Assert
            Assert.Equal("The White Bishop I'm working with is out of board.", outOfBoardException.Message);
        }
    }
}
