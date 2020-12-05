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
        public void IsErrorThereIsNoRoomForYourPieceWorkingCorrectly()
        {
            // Arrange
            Position position = core.CreatePosition(1, 2);
            Knight knight = core.CreateKnight(PieceColor.White);
            Board board = core.CreateEmptyBoard();
            board.PutAt(knight, position);
            Rook rook = core.CreateRook(PieceColor.White);

            // Act
            ThereIsNoRoomForYourPieceAtThisPositionException thereIsNoRoomError = Assert
                .Throws<ThereIsNoRoomForYourPieceAtThisPositionException>(() => { board.PutAt(rook, position); });

            // Assert
            Assert.Equal("You cannot put your Rook at position C2 because already exists a Knight in this square.", 
                thereIsNoRoomError.Message);
        }

        [Fact]
        public void IsPiecesOutOfBoardExceptionWorkingCorrectly()
        {
            // Arrange
            Bishop bishop = core.CreateBishop(PieceColor.White);
            Position position = core.CreatePosition(-1, 18);
            bishop.Position = position;

            // Act
            PieceOutOfBoardException outOfBoardException = Assert.Throws<PieceOutOfBoardException>(bishop.AssertPositionIsInsideLimits);

            // Assert
            Assert.Equal("The White Bishop I'm working with is out of board.", outOfBoardException.Message);
        }
    }
}
