using FunChess.Core.Enums;
using FunChess.Core.Factory;
using FunChess.Core.Models;
using FunChess.Core.Models.Pieces;
using System.Collections.Generic;
using Xunit;

namespace FunChess.XUnitTests.CoreTests.Models.Pieces
{
    public class RookTests
    {
        private readonly CoreFactory core;

        public RookTests()
        {
            core = Beyond.Core;
        }


        [Fact]
        public void IsAllowedSetBeenCalculatedCorrectlyWhenRookIsBlocked()
        {
            // Arrange
            Board board = core.CreateEmptyBoard();
            Rook rook = core.CreateRook(PieceColor.White);
            Position rookPosition = core.CreatePosition(2, 2);
            board.PutAt(rook, rookPosition);
            Pawn pawn = core.CreatePawn(PieceColor.White);
            Position pawnPosition = core.CreatePosition(5, 2);
            board.PutAt(pawn, pawnPosition);

            // Act
            bool[,] permissionMatrix = rook.GetPermissionMatrix(board);
            int amountOfPermitedPositions = rook.CountPermitedPositions();
            HashSet<Position> allowedSet = rook.GetAllowedSet();

            // Assert
            Assert.NotNull(permissionMatrix);
            Assert.Equal(11, amountOfPermitedPositions);
            AssertAllowedSetIsOkWhenRookIsBlocked(allowedSet);
        }

        [Fact]
        public void IsAllowedSetBeenCalculatedCorrectlyWhenRookIsThreatening()
        {
            // Arrange
            Board board = core.CreateEmptyBoard();
            Rook rook = core.CreateRook(PieceColor.White);
            Position rookPosition = core.CreatePosition(2, 2);
            board.PutAt(rook, rookPosition);
            Knight knight = core.CreateKnight(PieceColor.Black);
            Position knightPosition = core.CreatePosition(5, 2);
            board.PutAt(knight, knightPosition);

            // Act
            bool[,] permissionMatrix = rook.GetPermissionMatrix(board);
            int amountOfPermitedPositions = rook.CountPermitedPositions();
            HashSet<Position> allowedSet = rook.GetAllowedSet();

            // Assert
            Assert.NotNull(permissionMatrix);
            Assert.Equal(12, amountOfPermitedPositions);
            AssertAllowedSetIsOkWhenRookIsThreatening(allowedSet);
        }        

        [Fact]
        public void IsAllowedSetBeenCalculatedCorrectlyWhenRookIsFreeToMove()
        {
            // Arrange
            Board board = core.CreateEmptyBoard();
            Rook rook = core.CreateRook(PieceColor.White);
            Position rookPosition = core.CreatePosition(2, 2);
            board.PutAt(rook, rookPosition);

            // Act
            bool[,] permissionMatrix = rook.GetPermissionMatrix(board);
            int amountOfPermitedPositions = rook.CountPermitedPositions();
            HashSet<Position> allowedSet = rook.GetAllowedSet();

            // Assert
            Assert.NotNull(permissionMatrix);
            Assert.Equal(14, amountOfPermitedPositions);
            AssertAllowedSetIsOkWhenRookIsFreeToMove(allowedSet);
        }


        private void AssertAllowedSetIsOkWhenRookIsFreeToMove(HashSet<Position> allowedSet)
        {
            AssertAllowedSetIsOkWhenRookIsThreatening(allowedSet);
            Assert.Contains(core.CreatePosition(6, 2), allowedSet);
            Assert.Contains(core.CreatePosition(7, 2), allowedSet);
        }

        private void AssertAllowedSetIsOkWhenRookIsThreatening(HashSet<Position> allowedSet)
        {
            AssertAllowedSetIsOkWhenRookIsBlocked(allowedSet);
            Assert.Contains(core.CreatePosition(5, 2), allowedSet);
        }

        private void AssertAllowedSetIsOkWhenRookIsBlocked(HashSet<Position> allowedSet)
        {
            Assert.Contains(core.CreatePosition(2, 0), allowedSet);
            Assert.Contains(core.CreatePosition(2, 1), allowedSet);
            Assert.Contains(core.CreatePosition(2, 3), allowedSet);
            Assert.Contains(core.CreatePosition(2, 4), allowedSet);
            Assert.Contains(core.CreatePosition(2, 5), allowedSet);
            Assert.Contains(core.CreatePosition(2, 6), allowedSet);
            Assert.Contains(core.CreatePosition(2, 7), allowedSet);
            Assert.Contains(core.CreatePosition(0, 2), allowedSet);
            Assert.Contains(core.CreatePosition(1, 2), allowedSet);
            Assert.Contains(core.CreatePosition(3, 2), allowedSet);
            Assert.Contains(core.CreatePosition(4, 2), allowedSet);
        }
    }
}
