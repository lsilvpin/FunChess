using FunChess.Core.Enums;
using FunChess.Core.Factory;
using FunChess.Core.Models;
using FunChess.Core.Models.Pieces;
using System.Collections.Generic;
using Xunit;

namespace FunChess.XUnitTests.CoreTests.Models.Pieces
{
    public class PawnTests
    {
        private readonly CoreFactory core;

        public PawnTests()
        {
            core = Beyond.Core;
        }

        [Theory]
        [InlineData(PieceColor.White)]
        [InlineData(PieceColor.Black)]
        public void IsPawnPermitedPositionsBeenCalculatedCorrectlyWhenPawnIsAtInitialLineWithouAnyBlockOrThreatningFoe(PieceColor color)
        {
            // Arrange
            Board board = core.CreateEmptyBoard();
            Pawn whitePawn = core.CreatePawn(color);
            int increment = (color == PieceColor.White) ? +1 : -1;
            int initialLine = (color == PieceColor.White) ? 1 : 6;
            Position position = core.CreatePosition(initialLine, 2);
            board.PutAt(whitePawn, position);

            // Act
            bool[,] permissionMatrix = whitePawn.GetPermissionMatrix(board);
            int amountOfPermitedPositions = whitePawn.CountPermitedPositions();
            HashSet<Position> allowedSet = whitePawn.GetAllowedSet();

            // Assert
            Assert.NotNull(permissionMatrix);
            Assert.Equal(2, amountOfPermitedPositions);
            Assert.NotNull(allowedSet);
            Assert.Equal(2, allowedSet.Count);
            Assert.Contains(core.CreatePosition(initialLine + increment, 2), allowedSet);
            Assert.Contains(core.CreatePosition(initialLine + 2 * increment, 2), allowedSet);
        }

        [Theory]
        [InlineData(PieceColor.White)]
        [InlineData(PieceColor.Black)]
        public void IsPawnAllowedSetBeenConstructedCorrectlyWhenPawnIsInMidleOfBoardWithoutAnyBlockOrThreatningFoe(PieceColor color)
        {
            // Arrange
            Board board = core.CreateEmptyBoard();
            Pawn pawn = core.CreatePawn(color);
            Position initialPosition = core.CreatePosition(3, 3);
            board.PutAt(pawn, initialPosition);
            int increment = (color == PieceColor.White) ? +1 : -1;

            // Act
            bool[,] pawnPermitedMatrix = pawn.GetPermissionMatrix(board);
            int amountOfPermitedPositions = pawn.CountPermitedPositions();
            HashSet<Position> pawnAllowedSet = pawn.GetAllowedSet();

            // Assert
            Assert.NotNull(pawnPermitedMatrix);
            Assert.Equal(1, amountOfPermitedPositions);
            Assert.NotNull(pawnAllowedSet);
            Assert.Single(pawnAllowedSet);
            Assert.Contains(core.CreatePosition(initialPosition.Line + increment, initialPosition.Column), pawnAllowedSet);
        }
    }
}
