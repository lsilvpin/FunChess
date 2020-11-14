using FunChess.Core.Enums;
using FunChess.Core.Factory;
using FunChess.Core.Models;
using FunChess.Core.Models.Pieces;
using System.Collections.Generic;
using Xunit;

namespace FunChess.XUnitTests.CoreTests.Models.PiecesTests
{
    public class PawnTests
    {
        private readonly CoreFactory core;

        public PawnTests()
        {
            core = Beyond.Core;
        }

        [Fact]
        public void IsPawnPermitedPositionsBeenCalculatedCorrectlyWhenPawnIsAtInitialLineWithouAnyBlockOrThreatningFoe()
        {
            // Arrange
            Board board = core.CreateEmptyBoard();
            Pawn whitePawn = core.CreatePawn(PieceColor.White);
            board.PutAt(whitePawn, core.CreatePosition(1, 2));

            // Act
            bool[,] permissionMatrix = whitePawn.GetPermissionMatrix(board);
            int amountOfPermitedPositions = whitePawn.CountPermitedPositions();
            HashSet<Position> allowedSet = whitePawn.GetAllowedSet();

            // Assert
            Assert.NotNull(permissionMatrix);
            Assert.Equal(2, amountOfPermitedPositions);
            Assert.NotNull(allowedSet);
            Assert.Equal(2, allowedSet.Count);
            Assert.Contains(core.CreatePosition(2, 2), allowedSet);
            Assert.Contains(core.CreatePosition(3, 2), allowedSet);
        }
    }
}
