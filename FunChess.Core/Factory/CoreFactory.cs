using FunChess.Core.BusinessLogic;
using FunChess.Core.Enums;
using FunChess.Core.Models;
using FunChess.Core.Models.Pieces;
using FunChess.Core.Tools;

namespace FunChess.Core.Factory
{
    public class CoreFactory
    {
        private Brain brain;

        public CoreFactory()
        {
            brain = CreateBrain();
        }


        public Cloner CreateCloner()
        {
            return new Cloner(this);
        }

        public King CreateKing(PieceColor color)
        {
            return new King(this, color);
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
            return new Queen(this, color, brain);
        }

        public Position CreatePosition(int line, int column)
        {
            return new Position(line, column);
        }

        public Bishop CreateBishop(PieceColor color)
        {
            return new Bishop(this, color, brain);
        }

        public Knight CreateKnight(PieceColor color)
        {
            return new Knight(this, color);
        }

        public Board CreateEmptyBoard()
        {
            return new Board(this);
        }

        public Rook CreateRook(PieceColor color)
        {
            return new Rook(this, color, brain);
        }

        public Board CreateBoardWithOneKingAt(Position position)
        {
            Board board = CreateEmptyBoard();
            Piece king = CreateKing(PieceColor.White);
            board.PutAt(king, position);
            return board;
        }

        public EnPassant CreateEnPassant(EnPassantSide side)
        {
            return new EnPassant(side);
        }

        public Pawn CreatePawn(PieceColor color)
        {
            return new Pawn(this, color);
        }
    }
}
