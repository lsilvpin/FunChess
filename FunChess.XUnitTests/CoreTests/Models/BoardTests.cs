using FunChess.Core.Enums;
using FunChess.Core.Factory;
using FunChess.Core.Models;
using FunChess.Core.Models.Pieces;
using Xunit;

namespace FunChess.XUnitTests.CoreTests.Models
{
    public class BoardTests
    {
        private readonly CoreFactory core;

        public BoardTests()
        {
            core = Beyond.Core;
        }

        [Fact]
        public void CanWePutPiecesOnBoard()
        {
            // Arrange
            King king = core.CreateKing(PieceColor.White);
            Board board = core.CreateEmptyBoard();
            Position position = core.CreatePosition(0, 1);

            // Act
            board.PutAt(king, position);

            // Assert
            Assert.Equal(king, board.LookAt(position));
        }

        [Fact]
        public void CanWeTakePiecesFromBoard()
        {
            // Arrange
            Position position = core.CreatePosition(2, 3);
            Board boardWithOneKing = core.CreateBoardWithOneKingAt(position);

            // Act
            Piece piece = boardWithOneKing.TakeAt(position);

            // Assert
            Assert.NotNull(piece);
            Assert.Null(boardWithOneKing.LookAt(position));
            Assert.True(piece is King);
        }

        [Fact]
        public void CanWeLookPiecesAtBoard()
        {
            // Arrange
            Position position = core.CreatePosition(3, 4);
            Board board = core.CreateEmptyBoard();
            Bishop bishop = core.CreateBishop(PieceColor.Black);
            board.PutAt(bishop, position);

            // Act
            Piece piece = board.LookAt(position);

            // Assert
            Assert.NotNull(piece);
            Assert.Equal(bishop, piece);
            Assert.NotNull(board.Grid[position.Line, position.Column]);
            Assert.Equal(bishop, board.Grid[position.Line, position.Column]);
        }
    }
}
