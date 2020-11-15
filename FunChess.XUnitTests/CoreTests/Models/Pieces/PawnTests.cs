using FunChess.Core.Enums;
using FunChess.Core.Factory;
using FunChess.Core.Models;
using FunChess.Core.Models.Pieces;
using FunChess.XUnitTests.TestTools.Factory;
using FunChess.XUnitTests.TestTools.Helpers;
using System.Collections.Generic;
using Xunit;

namespace FunChess.XUnitTests.CoreTests.Models.Pieces
{
    public class PawnTests
    {
        private readonly CoreFactory core;
        private readonly TestFactory test;
        private readonly Board board;
        private readonly Pawn pawn;

        private readonly TestHelper testHelper;

        public PawnTests()
        {
            core = Beyond.Core;
            test = TestVortex.Test;
            testHelper = test.CreateTestHelper();
            board = core.CreateEmptyBoard();
            pawn = core.CreatePawn(PieceColor.White);
        }

        [Theory]
        [InlineData(PieceColor.White)]
        [InlineData(PieceColor.Black)]
        public void IsPawnPermitedPositionsBeenCalculatedCorrectlyWhenPawnHasTheOportunityToCaptureFoeByEnPassant(PieceColor color)
        {
            // Arrange
            int enPassantLine = (color == PieceColor.White) ? 4 : 3;
            int increment = (color == PieceColor.White) ? +1 : -1;
            pawn.Color = color;
            Position position = core.CreatePosition(enPassantLine, 5);
            board.PutAt(pawn, position);
            Pawn enemyPawn = core.CreatePawn(testHelper.SwitchColor(color));
            Position enemyPosition = core.CreatePosition(enPassantLine, 4);
            board.PutAt(enemyPawn, enemyPosition);
            board.EnPassant = core.CreateEnPassant(EnPassantSide.Left);

            // Act
            bool[,] pawnPermissionMatrix = pawn.GetPermissionMatrix(board);
            int amountOfPermitedPositions = pawn.CountPermitedPositions();
            HashSet<Position> pawnAllowedSet = pawn.GetAllowedSet();

            // Assert
            Assert.NotNull(pawnPermissionMatrix);
            Assert.Equal(2, amountOfPermitedPositions);
            Assert.NotNull(pawnAllowedSet);
            Assert.Equal(2, pawnAllowedSet.Count);
            Assert.Contains(core.CreatePosition(enPassantLine + increment, 5), pawnAllowedSet);
            Assert.Contains(core.CreatePosition(enPassantLine + increment, 4), pawnAllowedSet);
        }

        [Theory]
        [InlineData(PieceColor.White)]
        [InlineData(PieceColor.Black)]
        public void IsPawnPermitedPositionsBeenCalculatedCorrectlyWhenPawnIsThreatningEnemyPieces(PieceColor color)
        {
            // Arrange
            pawn.Color = color;
            Position pawnPosition = core.CreatePosition(3, 3);
            Rook rook = core.CreateRook(testHelper.SwitchColor(color));
            int increment = (color == PieceColor.White) ? +1 : -1;
            Position rookPosition = core.CreatePosition(pawnPosition.Line + increment, pawnPosition.Column + increment);
            board.PutAt(pawn, pawnPosition);
            board.PutAt(rook, rookPosition);

            // Act
            bool[,] pawnPermissionMatrix = pawn.GetPermissionMatrix(board);
            int amountOfPermitedPositions = pawn.CountPermitedPositions();
            HashSet<Position> pawnAllowedSet = pawn.GetAllowedSet();

            // Assert
            Assert.NotNull(pawnPermissionMatrix);
            Assert.Equal(2, amountOfPermitedPositions);
            Assert.NotNull(pawnAllowedSet);
            Assert.Equal(2, pawnAllowedSet.Count);
            if (color == PieceColor.White)
            {
                Assert.Contains(core.CreatePosition(4, 3), pawnAllowedSet);
                Assert.Contains(core.CreatePosition(4, 4), pawnAllowedSet);
            }
            else
            {
                Assert.Contains(core.CreatePosition(2, 3), pawnAllowedSet);
                Assert.Contains(core.CreatePosition(2, 2), pawnAllowedSet);
            }
        }

        [Theory]
        [InlineData(PieceColor.White)]
        [InlineData(PieceColor.Black)]
        public void IsPawnPermitedPositionsBeenCalculatedCorrectlyWhenPawnIsBeenBlocked(PieceColor color)
        {
            // Arrange
            pawn.Color = color;
            Position pawnPosition = core.CreatePosition(3, 2);
            board.PutAt(pawn, pawnPosition);
            Knight knight = core.CreateKnight(testHelper.SwitchColor(color));
            int increment = (color == PieceColor.White) ? +1 : -1;
            Position knightPosition = core.CreatePosition(pawnPosition.Line + increment, pawnPosition.Column);
            board.PutAt(knight, knightPosition);

            // Act
            bool[,] pawnPermitedMatrix = pawn.GetPermissionMatrix(board);
            int amountOfPermitedPositions = pawn.CountPermitedPositions();
            HashSet<Position> pawnAllowedSet = pawn.GetAllowedSet();

            // Assert
            Assert.NotNull(pawnPermitedMatrix);
            Assert.Equal(0, amountOfPermitedPositions);
            Assert.Empty(pawnAllowedSet);
        }

        [Theory]
        [InlineData(PieceColor.White)]
        [InlineData(PieceColor.Black)]
        public void IsPawnPermitedPositionsBeenCalculatedCorrectlyWhenPawnIsAtInitialLineWithouAnyBlockOrThreatningFoe(PieceColor color)
        {
            // Arrange
            pawn.Color = color;
            Pawn whitePawn = pawn;
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
            pawn.Color = color;
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
