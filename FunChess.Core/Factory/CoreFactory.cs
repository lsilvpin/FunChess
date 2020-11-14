using FunChess.Core.BusinessLogic;
using FunChess.Core.Enums;
using FunChess.Core.Models;
using FunChess.Core.Models.Pieces;

namespace FunChess.Core.Factory
{
    public class CoreFactory
    {
        private Brain brain;

        public CoreFactory()
        {
        }

        public King CreateKing(PieceColor color)
        {
            return new King(color);
        }

        public Brain CreateBrain()
        {
            if (brain == null)
            {
                brain = new Brain(this);
            }
            return brain;
        }

        public Queen CreateQueen(PieceColor color)
        {
            return new Queen(color);
        }

        public Coordinate CreateCoordinate(int line, int column)
        {
            return new Coordinate(line, column);
        }

        public Bishop CreateBishop(PieceColor color)
        {
            return new Bishop(color);
        }

        public Knight CreateKnight(PieceColor color)
        {
            return new Knight(color);
        }

        public Board CreateEmptyBoard()
        {
            return new Board();
        }

        public Rook CreateRook(PieceColor color)
        {
            return new Rook(color);
        }

        public Board CreteBoardWithOneKingAt(int line, int column)
        {
            Board board = CreateEmptyBoard();
            Piece king = CreateKing(PieceColor.White);
            board.PutAt(king, line, column);
            return board;
        }

        public Pawn CreatePawn(PieceColor color)
        {
            return new Pawn(color);
        }
    }
}
