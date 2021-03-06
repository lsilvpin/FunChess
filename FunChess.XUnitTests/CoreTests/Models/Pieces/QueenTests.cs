﻿using FunChess.Core.Enums;
using FunChess.Core.Factory;
using FunChess.Core.Models;
using FunChess.Core.Models.Pieces;
using System.Collections.Generic;
using Xunit;

namespace FunChess.XUnitTests.CoreTests.Models.Pieces
{
    public class QueenTests
    {
        private readonly CoreFactory core;

        public QueenTests()
        {
            core = Beyond.Core;
        }


        [Fact]
        public void IsAllowedSetBeenCalculatedCorrectlyWhenQueenIsThreatening()
        {
            // Arrange
            Board board = core.CreateEmptyBoard();
            Queen queen = core.CreateQueen(PieceColor.White);
            Position queenPosition = core.CreatePosition(3, 4);
            Rook rook = core.CreateRook(PieceColor.Black);
            Position rookPosition = core.CreatePosition(5, 4);
            board.PutAt(queen, queenPosition);
            board.PutAt(rook, rookPosition);

            // Act
            bool[,] permissionMatrix = queen.GetPermissionMatrix(board);
            int amountOfPermitedPositions = queen.CountPermitedPositions();
            HashSet<Position> allowedSet = queen.GetAllowedSet();

            // Assert
            Assert.NotNull(permissionMatrix);
            Assert.Equal(25, amountOfPermitedPositions);
            AssertAllowedSetIsOkWhenQueenIsThreatening(allowedSet);
        }

        [Fact]
        public void IsAllowedSetBeenCalculatedCorrectlyWhenQueenIsBlocked()
        {
            // Arrange
            Board board = core.CreateEmptyBoard();
            Queen queen = core.CreateQueen(PieceColor.White);
            Position queenPosition = core.CreatePosition(3, 4);
            Knight knight = core.CreateKnight(PieceColor.White);
            Position knightPosition = core.CreatePosition(5, 4);
            board.PutAt(queen, queenPosition);
            board.PutAt(knight, knightPosition);

            // Act
            bool[,] permissionMatrix = queen.GetPermissionMatrix(board);
            int amountOfPermitedPositions = queen.CountPermitedPositions();
            HashSet<Position> allowedSet = queen.GetAllowedSet();

            // Assert
            Assert.NotNull(permissionMatrix);
            Assert.Equal(24, amountOfPermitedPositions);
            AssertAllowedSetIsOkWhenQueenIsBlocked(allowedSet);
        }

        [Fact]
        public void IsAllowedSetBeenCalculatedCorrectlyWhenQueenIsFreeToMove()
        {
            // Arrange
            Board board = core.CreateEmptyBoard();
            Queen queen = core.CreateQueen(PieceColor.White);
            Position position = core.CreatePosition(3, 4);
            board.PutAt(queen, position);

            // Act
            bool[,] permissionMatrix = queen.GetPermissionMatrix(board);
            int amountOfPermitedPositions = queen.CountPermitedPositions();
            HashSet<Position> allowedSet = queen.GetAllowedSet();

            // Assert
            Assert.NotNull(permissionMatrix);
            Assert.Equal(27, amountOfPermitedPositions);
            AssertAllowedSetIsOkWhenQueenIsFreeToMove(allowedSet);
        }


        private void AssertAllowedSetIsOkWhenQueenIsFreeToMove(HashSet<Position> allowedSet)
        {
            Assert.Contains(core.CreatePosition(6, 4), allowedSet);
            Assert.Contains(core.CreatePosition(7, 4), allowedSet);
            AssertAllowedSetIsOkWhenQueenIsThreatening(allowedSet);
        }

        private void AssertAllowedSetIsOkWhenQueenIsThreatening(HashSet<Position> allowedSet)
        {
            Assert.Contains(core.CreatePosition(5, 4), allowedSet);
            AssertAllowedSetIsOkWhenQueenIsBlocked(allowedSet);
        }

        private void AssertAllowedSetIsOkWhenQueenIsBlocked(HashSet<Position> allowedSet)
        {
            Assert.Contains(core.CreatePosition(0, 4), allowedSet);
            Assert.Contains(core.CreatePosition(1, 4), allowedSet);
            Assert.Contains(core.CreatePosition(2, 4), allowedSet);
            Assert.Contains(core.CreatePosition(4, 4), allowedSet);
            Assert.Contains(core.CreatePosition(3, 0), allowedSet);
            Assert.Contains(core.CreatePosition(3, 1), allowedSet);
            Assert.Contains(core.CreatePosition(3, 2), allowedSet);
            Assert.Contains(core.CreatePosition(3, 3), allowedSet);
            Assert.Contains(core.CreatePosition(3, 5), allowedSet);
            Assert.Contains(core.CreatePosition(3, 6), allowedSet);
            Assert.Contains(core.CreatePosition(3, 7), allowedSet);
            Assert.Contains(core.CreatePosition(0, 1), allowedSet);
            Assert.Contains(core.CreatePosition(1, 2), allowedSet);
            Assert.Contains(core.CreatePosition(2, 3), allowedSet);
            Assert.Contains(core.CreatePosition(4, 5), allowedSet);
            Assert.Contains(core.CreatePosition(5, 6), allowedSet);
            Assert.Contains(core.CreatePosition(6, 7), allowedSet);
            Assert.Contains(core.CreatePosition(0, 7), allowedSet);
            Assert.Contains(core.CreatePosition(1, 6), allowedSet);
            Assert.Contains(core.CreatePosition(2, 5), allowedSet);
            Assert.Contains(core.CreatePosition(4, 3), allowedSet);
            Assert.Contains(core.CreatePosition(5, 2), allowedSet);
            Assert.Contains(core.CreatePosition(6, 1), allowedSet);
            Assert.Contains(core.CreatePosition(7, 0), allowedSet);
        }
    }
}
