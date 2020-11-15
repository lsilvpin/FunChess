using FunChess.Core.Enums;
using FunChess.Core.Factory;
using FunChess.Core.Models;
using FunChess.Core.Models.Pieces;
using FunChess.XUnitTests.TestTools.Factory;
using FunChess.XUnitTests.TestTools.Helpers;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FunChess.XUnitTests.CoreTests.Models
{
    public class BoardTests
    {
        private readonly CoreFactory core;
        private readonly TestFactory test;
        private readonly TestUtils testHelper;

        public BoardTests()
        {
            core = Beyond.Core;
            test = TestVortex.Test;
            testHelper = test.CreateTestHelper();
        }

        [Fact]
        public void CanWePutPiecesOnBoard()
        {
            // Arrange
            King king = core.CreateKing(PieceColor.White);
            Board board = core.CreateEmptyBoard();
            Position position = core.CreatePosition(0, 1);

            // Act
            board.PutAt(king, position);

            // Assert
            Assert.Equal(king, board.LookAt(position));
        }

        [Fact]
        public void CanWeTakePiecesFromBoard()
        {
            // Arrange
            Position position = core.CreatePosition(2, 3);
            Board boardWithOneKing = core.CreateBoardWithOneKingAt(position);

            // Act
            Piece piece = boardWithOneKing.TakeAt(position);

            // Assert
            Assert.NotNull(piece);
            Assert.Null(boardWithOneKing.LookAt(position));
            Assert.True(piece is King);
        }

        [Fact]
        public void CanWeLookPiecesAtBoard()
        {
            // Arrange
            List<Position> positions = PrvArrangePositions();
            List<Piece> pieces = PrvArrangePieces();
            Board board = core.CreateEmptyBoard();
            PrvArrangeBoard(board, pieces, positions);

            // Act
            List<Piece> clones = PrvLookAtArrangedPositions(board, positions);

            // Assert
            PrvAssertAllClonesAreBehavingCorrectly(board, clones, positions);
        }

        #region Private helpers
        #region LookAt helpers
        private void PrvAssertAllClonesAreBehavingCorrectly(Board board, List<Piece> clones, List<Position> positions)
        {
            Enumerable.Range(0, 7).ToList().ForEach(index => PrvAssertIsOkWhatWeLookingAt(board, clones, positions, index));
        }

        private List<Piece> PrvLookAtArrangedPositions(Board board, List<Position> positions)
        {
            return new List<Piece>()
            {
                board.LookAt(positions[0]),
                board.LookAt(positions[1]),
                board.LookAt(positions[2]),
                board.LookAt(positions[3]),
                board.LookAt(positions[4]),
                board.LookAt(positions[5]),
                board.LookAt(positions[6])
            };
        }

        private void PrvArrangeBoard(Board board, List<Piece> pieces, List<Position> positions)
        {
            board.PutAt(pieces[0], positions[0]);
            board.PutAt(pieces[1], positions[1]);
            board.PutAt(pieces[2], positions[2]);
            board.PutAt(pieces[3], positions[3]);
            board.PutAt(pieces[4], positions[4]);
            board.PutAt(pieces[5], positions[5]);
        }

        private List<Piece> PrvArrangePieces()
        {
            return new List<Piece>()
            {
                core.CreateKing(PieceColor.White),
                core.CreateQueen(PieceColor.Black),
                core.CreateBishop(PieceColor.Black),
                core.CreateKnight(PieceColor.White),
                core.CreateRook(PieceColor.White),
                core.CreatePawn(PieceColor.Black)
            };
        }

        private List<Position> PrvArrangePositions()
        {
            return new List<Position>()
            {
                core.CreatePosition(0, 3),
                core.CreatePosition(2, 3),
                core.CreatePosition(1, 2),
                core.CreatePosition(3, 2),
                core.CreatePosition(4, 2),
                core.CreatePosition(5, 6),
                core.CreatePosition(7, 7)
            };
        }

        private void PrvAssertIsOkWhatWeLookingAt(Board board, List<Piece> clones, List<Position> positions, int index)
        {
            Position position = positions[index];
            Piece originalPiece = board.Grid[position.Line, position.Column];
            Piece clonedPiece = clones[index];

            if (index < 6)
            {
                Assert.NotNull(position);
                Assert.NotNull(originalPiece);
                Assert.NotNull(clonedPiece);
                Assert.Equal(originalPiece, clonedPiece);
                PrvChangePieceColor(originalPiece);
                Assert.NotEqual(originalPiece, clonedPiece);
            }
            else
            {
                Assert.NotNull(position);
                Assert.Null(originalPiece);
                Assert.Null(clonedPiece);
                Assert.Equal(originalPiece, clonedPiece);
            }
        }

        private void PrvChangePieceColor(Piece originalPiece)
        {
            originalPiece.Color = testHelper.SwitchColor(originalPiece.Color);
        }
        #endregion
        #endregion
    }
}
