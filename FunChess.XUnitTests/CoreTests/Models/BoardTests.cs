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

            emptyBoard.PutAt(piece, core.CreatePosition(0, 1));

            Assert.Equal(piece, emptyBoard.Grid[0, 1]);
        }

        [Fact]
        public void CanWeTakePiecesAtBoard()
        {
            Position position = core.CreatePosition(2, 3);
            Board boardWithOneKing = core.CreateBoardWithOneKingAt(position);
            Piece piece = boardWithOneKing.TakeAt(position);

            Assert.NotNull(piece);
            Assert.Null(boardWithOneKing.At(position));
        }
    }
}
