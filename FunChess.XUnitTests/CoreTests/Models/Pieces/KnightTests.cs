﻿using FunChess.Core.Enums;
using FunChess.Core.Factory;
using FunChess.Core.Models;
using FunChess.Core.Models.Pieces;
using System.Collections.Generic;
using Xunit;

namespace FunChess.XUnitTests.CoreTests.Models.Pieces
{
    public class KnightTests
    {
        private readonly CoreFactory core;

        public KnightTests()
        {
            core = Beyond.Core;
        }


        [Fact]
        public void IsAllowedSetBeenCalculatedCorrectlyWhenKnightIsThreatening()
        {
            // Arrange
            Board board = core.CreateEmptyBoard();
            Knight knight = core.CreateKnight(PieceColor.Black);
            Position knightPosition = core.CreatePosition(3, 3);
            board.PutAt(knight, knightPosition);
            Pawn pawn = core.CreatePawn(PieceColor.White);
            Position pawnPosition = core.CreatePosition(5, 4);
            board.PutAt(pawn, pawnPosition);

            // Act
            bool[,] permissionMatrix = knight.GetPermissionMatrix(board);
            int amountOfPermitedPositions = knight.CountPermitedPositions();
            HashSet<Position> allowedSet = knight.GetAllowedSet();

            // Assert
            Assert.NotNull(permissionMatrix);
            Assert.Equal(8, amountOfPermitedPositions);
            AssertAllowedSetIsOkWhenKnightIsFreeToMove(allowedSet);
        }

        [Fact]
        public void IsAllowedSetBeenCalculatedCorrectlyWhenKnightIsBlocked()
        {
            // Arrange
            Board board = core.CreateEmptyBoard();
            Knight knight = core.CreateKnight(PieceColor.White);
            Position knightPosition = core.CreatePosition(3, 3);
            board.PutAt(knight, knightPosition);
            Pawn pawn = core.CreatePawn(PieceColor.White);
            Position pawnPosition = core.CreatePosition(5, 4);
            board.PutAt(pawn, pawnPosition);

            // Act
            bool[,] permissionMatrix = knight.GetPermissionMatrix(board);
            int amountOfPermitedPositions = knight.CountPermitedPositions();
            HashSet<Position> permitedPositions = knight.GetAllowedSet();

            // Assert
            Assert.NotNull(permissionMatrix);
            Assert.Equal(7, amountOfPermitedPositions);
            AssertAllowedSetIsOkWhenKnightIsBlocked(permitedPositions);
        }

        [Fact]
        public void IsAllowedSetBeenCalculatedCorrectlyWhenKnightIsFreeToMove()
        {
            // Arrange
            Board board = core.CreateEmptyBoard();
            Knight knight = core.CreateKnight(PieceColor.White);
            Position position = core.CreatePosition(3, 3);
            board.PutAt(knight, position);

            // Act
            bool[,] permissionMatrix = knight.GetPermissionMatrix(board);
            int amountOfPermitedPositions = knight.CountPermitedPositions();
            HashSet<Position> permitedPositions = knight.GetAllowedSet();

            // Assert
            Assert.NotNull(permissionMatrix);
            Assert.Equal(8, amountOfPermitedPositions);
            AssertAllowedSetIsOkWhenKnightIsFreeToMove(permitedPositions);
        }


        private void AssertAllowedSetIsOkWhenKnightIsFreeToMove(HashSet<Position> permitedPositions)
        {
            Assert.Contains(core.CreatePosition(5, 4), permitedPositions);
            AssertAllowedSetIsOkWhenKnightIsBlocked(permitedPositions);
        }

        private void AssertAllowedSetIsOkWhenKnightIsBlocked(HashSet<Position> permitedPositions)
        {
            Assert.Contains(core.CreatePosition(4, 1), permitedPositions);
            Assert.Contains(core.CreatePosition(5, 2), permitedPositions);
            Assert.Contains(core.CreatePosition(4, 5), permitedPositions);
            Assert.Contains(core.CreatePosition(2, 5), permitedPositions);
            Assert.Contains(core.CreatePosition(1, 4), permitedPositions);
            Assert.Contains(core.CreatePosition(1, 2), permitedPositions);
            Assert.Contains(core.CreatePosition(2, 1), permitedPositions);
        }
    }
}
