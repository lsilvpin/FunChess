using FunChess.Core.Enums;
using FunChess.Core.Factory;
using FunChess.Core.Models;
using FunChess.Core.Models.Pieces;
using System;
using System.Linq;
using Xunit;

namespace FunChess.XUnitTests.CoreTests.Models.PiecesTests
{
    public class KingTests
    {
        private readonly CoreFactory core;

        public KingTests()
        {
            core = Beyond.Core;
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 3)]
        public void IsKingPermissionMatrixBeenCalculatedCorrectly(int line, int column)
        {
            Board board = core.CreateEmptyBoard();
            Enumerable.Range(0, 7).ToList().ForEach(j => board.PutAt(core.CreatePawn(PieceColor.Black), core.CreatePosition(4, j)));
            King king = core.CreateKing(PieceColor.White);
            board.PutAt(king, core.CreatePosition(line, column));

            bool[,] kingPermissionMatrix = king.GetPermissionMatrix(board);

            if (line == 1)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (Math.Abs(i - king.Position.Line) <= 1 && Math.Abs(j - king.Position.Column) <= 1)
                        {
                            Assert.True(kingPermissionMatrix[i, j]);
                        }
                        else
                        {
                            Assert.False(kingPermissionMatrix[i, j]);
                        }
                    }
                }
            }
        }
    }
}
