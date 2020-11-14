﻿using FunChess.Core.Enums;
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

        [Theory]
        [InlineData(PieceColor.White)]
        [InlineData(PieceColor.Black)]
        public void IsPawnPermitedPositionsBeenCalculatedCorrectlyWhenPawnIsAtInitialLineWithouAnyBlockOrThreatningFoe(PieceColor color)
        {
            // Arrange
            Board board = core.CreateEmptyBoard();
            Pawn whitePawn = core.CreatePawn(PieceColor.White);
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
    }
}
