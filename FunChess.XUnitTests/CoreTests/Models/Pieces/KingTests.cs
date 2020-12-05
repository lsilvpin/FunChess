using FunChess.Core.Enums;
using FunChess.Core.Factory;
using FunChess.Core.Models;
using FunChess.Core.Models.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FunChess.XUnitTests.CoreTests.Models.Pieces
{
    public class KingTests
    {
        private readonly CoreFactory core;

        public KingTests()
        {
            core = Beyond.Core;
        }


        [Fact]
        public void IsAllowedSetBeenCalculatedCorrectlyWhenKingIsThreatening()
        {
            // Arrange
            Board board = core.CreateEmptyBoard();
            King king = core.CreateKing(PieceColor.White);
            Position kingPosition = core.CreatePosition(3, 4);
            Knight knight = core.CreateKnight(PieceColor.Black);
            Position knightPosition = core.CreatePosition(4, 4);
            board.PutAt(king, kingPosition);
            board.PutAt(knight, knightPosition);

            // Act
            bool[,] permissionMatrix = king.GetPermissionMatrix(board);
            int amountOfPermitedPositions = king.CountPermitedPositions();
            HashSet<Position> allowedSet = king.GetAllowedSet();

            // Assert
            Assert.NotNull(permissionMatrix);
            Assert.Equal(8, amountOfPermitedPositions);
            PrvAssertAllowedSetIsOkWhenKingIsFreeToMove(allowedSet);
        }

        [Fact]
        public void IsAllowedSetBeenCalculatedCorrectlyWhenKingIsBlocked()
        {
            // Arrange
            Board board = core.CreateEmptyBoard();
            King king = core.CreateKing(PieceColor.White);
            Position kingPosition = core.CreatePosition(3, 4);
            Knight knight = core.CreateKnight(PieceColor.White);
            Position knightPosition = core.CreatePosition(4, 4);
            board.PutAt(king, kingPosition);
            board.PutAt(knight, knightPosition);

            // Act
            bool[,] permissionMatrix = king.GetPermissionMatrix(board);
            int amountOfPermitedPositions = king.CountPermitedPositions();
            HashSet<Position> allowedSet = king.GetAllowedSet();

            // Assert
            Assert.NotNull(permissionMatrix);
            Assert.Equal(7, amountOfPermitedPositions);
            PrvAssertAllowedSetIsOkWhenKingIsBlocked(allowedSet);
        }

        [Fact]
        public void IsAllowedSetBeenCalculatedCorrectlyWhenKingIsFreeToMove()
        {
            // Arrange
            Board board = core.CreateEmptyBoard();
            King king = core.CreateKing(PieceColor.White);
            Position position = core.CreatePosition(3, 4);
            board.PutAt(king, position);

            // Act
            bool[,] permissionMatrix = king.GetPermissionMatrix(board);
            int amountOfPermitedPositions = king.CountPermitedPositions();
            HashSet<Position> allowedSet = king.GetAllowedSet();

            // Assert
            Assert.NotNull(permissionMatrix);
            Assert.Equal(8, amountOfPermitedPositions);
            PrvAssertAllowedSetIsOkWhenKingIsFreeToMove(allowedSet);
        }

        private void PrvAssertAllowedSetIsOkWhenKingIsFreeToMove(HashSet<Position> allowedSet)
        {
            Assert.Contains(core.CreatePosition(4, 4), allowedSet);
            PrvAssertAllowedSetIsOkWhenKingIsBlocked(allowedSet);
        }

        private void PrvAssertAllowedSetIsOkWhenKingIsBlocked(HashSet<Position> allowedSet)
        {
            Assert.Contains(core.CreatePosition(4, 5), allowedSet);
            Assert.Contains(core.CreatePosition(3, 5), allowedSet);
            Assert.Contains(core.CreatePosition(2, 5), allowedSet);
            Assert.Contains(core.CreatePosition(2, 4), allowedSet);
            Assert.Contains(core.CreatePosition(2, 3), allowedSet);
            Assert.Contains(core.CreatePosition(3, 3), allowedSet);
            Assert.Contains(core.CreatePosition(4, 3), allowedSet);
        }
    }
}
