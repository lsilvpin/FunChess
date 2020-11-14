using FunChess.Core.Enums;
using FunChess.Core.Factory;
using FunChess.Core.Models;
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
            Piece piece = core.CreateKing(PieceColor.White);
            Board emptyBoard = core.CreateEmptyBoard();

            emptyBoard.PutAt(piece, 0, 1);

            Assert.Equal(piece, emptyBoard.Grid[0, 1]);
        }

        [Fact]
        public void CanWeTakePiecesAtBoard()
        {
            Board boardWithOneKing = core.CreteBoardWithOneKingAt(2, 3);
            Piece piece = boardWithOneKing.TakeAt(2, 3);

            Assert.NotNull(piece);
            Assert.Null(boardWithOneKing.Grid[2, 3]);
        }
    }
}
