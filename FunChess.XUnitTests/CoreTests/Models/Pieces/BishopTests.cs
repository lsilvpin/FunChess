using FunChess.Core.Enums;
using FunChess.Core.Factory;
using FunChess.Core.Models;
using FunChess.Core.Models.Pieces;
using System.Collections.Generic;
using Xunit;

namespace FunChess.XUnitTests.CoreTests.Models.Pieces
{
    public class BishopTests
    {
        private readonly CoreFactory core;

        public BishopTests()
        {
            core = Beyond.Core;
        }


        [Fact]
        public void IsAllowedSetBeenCalculatedCorrectlyWhenBishopIsThreatening()
        {
            // Arrange
            Board board = core.CreateEmptyBoard();
            Bishop whiteBishop = core.CreateBishop(PieceColor.White);
            Position bishopPosition = core.CreatePosition(2, 2);
            board.PutAt(whiteBishop, bishopPosition);
            Pawn blackPawn = core.CreatePawn(PieceColor.Black);
            Position pawnPosition = core.CreatePosition(4, 4);
            board.PutAt(blackPawn, pawnPosition);

            // Act
            bool[,] permissionMatrix = whiteBishop.GetPermissionMatrix(board);
            int amountOfAllowedPositions = whiteBishop.CountPermitedPositions();
            HashSet<Position> permitedPositions = whiteBishop.GetAllowedSet();

            // Assert
            Assert.NotNull(permissionMatrix);
            Assert.Equal(8, amountOfAllowedPositions);
            AssertPositionsIsOkInCaseBishopIsThreatening(permitedPositions);
        }

        [Fact]
        public void IsAllowedSetBeenCalculatedCorrectlyWhenBishopIsBlocked()
        {
            // Arrange
            Board board = core.CreateEmptyBoard();
            Bishop bishop = core.CreateBishop(PieceColor.White);
            Pawn pawn = core.CreatePawn(PieceColor.White);
            Position bishopPosition = core.CreatePosition(1, 1);
            Position pawnPosition = core.CreatePosition(3, 3);

            board.PutAt(pawn, pawnPosition);
            board.PutAt(bishop, bishopPosition);

            // Act
            bool[,] bishopPermissionMatrix = bishop.GetPermissionMatrix(board);
            int amountOfPermitedPositions = bishop.CountPermitedPositions();
            HashSet<Position> bishopAllowedSet = bishop.GetAllowedSet();

            // Assert
            Assert.NotNull(bishopPermissionMatrix);
            Assert.Equal(4, amountOfPermitedPositions);
            Assert.NotNull(bishopAllowedSet);
            AssertPositionsIsOkInCaseBishopIsBlocked(bishopAllowedSet);
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
            AssertPositionsIsOkInCaseBishopIsFree(bishopAllowedSet);
        }


        private void AssertPositionsIsOkInCaseBishopIsThreatening(HashSet<Position> permitedPositions)
        {
            Assert.Contains(core.CreatePosition(3, 3), permitedPositions);
            Assert.Contains(core.CreatePosition(4, 4), permitedPositions);
            Assert.Contains(core.CreatePosition(3, 1), permitedPositions);
            Assert.Contains(core.CreatePosition(4, 0), permitedPositions);
            Assert.Contains(core.CreatePosition(3, 1), permitedPositions);
            Assert.Contains(core.CreatePosition(4, 0), permitedPositions);
            Assert.Contains(core.CreatePosition(1, 1), permitedPositions);
            Assert.Contains(core.CreatePosition(0, 0), permitedPositions);
        }

        private void AssertPositionsIsOkInCaseBishopIsBlocked(HashSet<Position> bishopAllowedSet)
        {
            Assert.Contains(core.CreatePosition(0, 0), bishopAllowedSet);
            Assert.Contains(core.CreatePosition(2, 2), bishopAllowedSet);
            Assert.Contains(core.CreatePosition(2, 0), bishopAllowedSet);
            Assert.Contains(core.CreatePosition(0, 2), bishopAllowedSet);
        }

        private void AssertPositionsIsOkInCaseBishopIsFree(HashSet<Position> bishopAllowedSet)
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
    }
}
