using FunChess.Core.Enums;
using FunChess.Core.Factory;
using FunChess.Core.Models;
using FunChess.Core.Models.Pieces;
using FunChess.XUnitTests.TestTools.Factory;
using System.Collections.Generic;
using Xunit;

namespace FunChess.XUnitTests.CoreTests.Models.Pieces
{
    public class RookTests
    {
        private readonly CoreFactory core;
        private readonly TestFactory test;

        public RookTests()
        {
            core = Beyond.Core;
            test = TestVortex.Test;
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
            PrvAssertAllowedSetIsOkWhenRookIsBlocked(allowedSet);
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
            PrvAssertAllowedSetIsOkWhenRookIsThreatening(allowedSet);
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
            PrvAssertAllowedSetIsOkWhenRookIsFreeToMove(allowedSet);
        }

        private void PrvAssertAllowedSetIsOkWhenRookIsFreeToMove(HashSet<Position> allowedSet)
        {
            PrvAssertAllowedSetIsOkWhenRookIsThreatening(allowedSet);
            Assert.Contains(core.CreatePosition(6, 2), allowedSet);
            Assert.Contains(core.CreatePosition(7, 2), allowedSet);
        }

        private void PrvAssertAllowedSetIsOkWhenRookIsThreatening(HashSet<Position> allowedSet)
        {
            PrvAssertAllowedSetIsOkWhenRookIsBlocked(allowedSet);
            Assert.Contains(core.CreatePosition(5, 2), allowedSet);
        }

        private void PrvAssertAllowedSetIsOkWhenRookIsBlocked(HashSet<Position> allowedSet)
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
