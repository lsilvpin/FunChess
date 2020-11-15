using FunChess.Core.Enums;
using FunChess.Core.Factory;
using FunChess.Core.Models;
using FunChess.Core.Models.Pieces;
using FunChess.XUnitTests.TestTools.Factory;
using System.Collections.Generic;
using Xunit;

namespace FunChess.XUnitTests.CoreTests.Models.Pieces
{
    public class BishopTests
    {
        private readonly CoreFactory core;
        private readonly TestFactory test;

        public BishopTests()
        {
            core = Beyond.Core;
            test = TestVortex.Test;
        }

        [Fact]
        public void IsPermissionBeenCalculatedCorrectlyWhenBishopIsCompletlyFreeToMove()
        {
            // Arrange
            Board board = core.CreateEmptyBoard();
            Bishop bishop = core.CreateBishop(PieceColor.White);
            Position position = core.CreatePosition(3, 3);
            board.PutAt(bishop, position);

            // Act
            bool[,] bishopPermissionMatrix = bishop.GetPermissionMatrix(board);
            int amountOfPermitedPositions = bishop.CountPermitedPositions();
            HashSet<Position> bishopAllowedSet = bishop.GetAllowedSet();

            // Assert
            Assert.NotNull(bishopPermissionMatrix);
            Assert.Equal(13, amountOfPermitedPositions);
            Assert.NotNull(bishopAllowedSet);
            Assert.Equal(13, bishopAllowedSet.Count);
            PrvAssertPositionsInAllowedSetAreOk(bishopAllowedSet);
        }

        #region Private helpers
        private void PrvAssertPositionsInAllowedSetAreOk(HashSet<Position> bishopAllowedSet)
        {
            Assert.Contains(core.CreatePosition(0, 0), bishopAllowedSet);
            Assert.Contains(core.CreatePosition(1, 1), bishopAllowedSet);
            Assert.Contains(core.CreatePosition(2, 2), bishopAllowedSet);
            Assert.Contains(core.CreatePosition(4, 4), bishopAllowedSet);
            Assert.Contains(core.CreatePosition(5, 5), bishopAllowedSet);
            Assert.Contains(core.CreatePosition(6, 6), bishopAllowedSet);
            Assert.Contains(core.CreatePosition(7, 7), bishopAllowedSet);
            Assert.Contains(core.CreatePosition(2, 4), bishopAllowedSet);
            Assert.Contains(core.CreatePosition(1, 5), bishopAllowedSet);
            Assert.Contains(core.CreatePosition(0, 6), bishopAllowedSet);
            Assert.Contains(core.CreatePosition(4, 2), bishopAllowedSet);
            Assert.Contains(core.CreatePosition(5, 1), bishopAllowedSet);
            Assert.Contains(core.CreatePosition(6, 0), bishopAllowedSet);
        }
        #endregion
    }
}
