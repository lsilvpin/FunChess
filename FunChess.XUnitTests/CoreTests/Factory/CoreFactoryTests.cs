using FunChess.Core.BusinessLogic;
using FunChess.Core.Enums;
using FunChess.Core.Factory;
using FunChess.Core.Models;
using FunChess.Core.Tools;
using System.Collections.Generic;
using Xunit;

namespace FunChess.XUnitTests.CoreTests.Factory
{
    public class CoreFactoryTests
    {
        private readonly CoreFactory core;

        public CoreFactoryTests()
        {
            core = Beyond.Core;
        }

        [Fact]
        public void IsClonerBeenCreated()
        {
            Cloner cloner = core.CreateCloner();

            Assert.NotNull(cloner);
        }

        [Fact]
        public void IsCoordinatesBeenCreated()
        {
            Position coordinate = core.CreatePosition(2, 3);

            Assert.NotNull(coordinate);
        }

        [Fact]
        public void IsBrainBeenCreated()
        {
            Brain brain = core.CreateBrain();

            Assert.NotNull(brain);
        }

        [Fact]
        public void IsPiecesBeenCreated()
        {
            List<Piece> pieces = PrvCreatePieces();

            Assert.Equal(12, pieces.Count);
        }

        [Fact]
        public void IsEmptyBoardBeenCreated()
        {
            Board board = core.CreateEmptyBoard();

            Assert.NotNull(board);
        }

        #region Private helpers

        private List<Piece> PrvCreatePieces()
        {
            return new List<Piece>
            {
                core.CreateKing(PieceColor.White),
                core.CreateKing(PieceColor.Black),
                core.CreateQueen(PieceColor.White),
                core.CreateQueen(PieceColor.Black),
                core.CreateBishop(PieceColor.White),
                core.CreateBishop(PieceColor.Black),
                core.CreateKnight(PieceColor.White),
                core.CreateKnight(PieceColor.Black),
                core.CreateRook(PieceColor.White),
                core.CreateRook(PieceColor.Black),
                core.CreatePawn(PieceColor.White),
                core.CreatePawn(PieceColor.Black)
            };
        }

        #endregion
    }
}
