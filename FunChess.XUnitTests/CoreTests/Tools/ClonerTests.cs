using FunChess.Core.Enums;
using FunChess.Core.Factory;
using FunChess.Core.Models;
using FunChess.Core.Models.Pieces;
using FunChess.Core.Tools;
using Xunit;

namespace FunChess.XUnitTests.CoreTests.Tools
{
    public class ClonerTests
    {
        private readonly CoreFactory core;
        private readonly Cloner cloner;

        public ClonerTests()
        {
            core = Beyond.Core;
            cloner = core.CreateCloner();
        }

        [Fact]
        public void IsClonerWorkingCorrectly()
        {
            // Arrange
            Board board = core.CreateEmptyBoard();
            Rook rook = core.CreateRook(PieceColor.White);
            Position position = core.CreatePosition(1, 2);
            board.PutAt(rook, position);

            // Act
            Board clonedBoard = cloner.Clone<Board>(board);
            Rook clonedRook = cloner.Clone<Rook>(rook);
            Position clonedPosition = cloner.Clone<Position>(position);

            // Assert
            Assert.NotNull(clonedBoard);
            Assert.NotNull(clonedRook);
            Assert.NotNull(clonedPosition);
            Assert.Equal(clonedRook, rook);
            Assert.Equal(clonedPosition, position);
            Assert.Equal(clonedBoard, board);

            rook.Color = PieceColor.Black;
            Assert.NotEqual(clonedRook, rook);
        }
    }
}
