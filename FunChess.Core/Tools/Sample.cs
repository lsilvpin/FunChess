using FunChess.Core.Enums;
using FunChess.Core.Factory;
using FunChess.Core.Models;
using FunChess.Core.Models.Pieces;

namespace FunChess.Core.Tools
{
    public static class Sample
    {
        private static CoreFactory core { get { return Beyond.Core; } }


        public static Board CreateDefaultBoard()
        {
            Board board = core.CreateEmptyBoard();

            PopulateBoardWithPawns(board);
            PopulateBoardWithRooks(board);
            PopulateBoardWithKnights(board);
            PopulateBoardWithBishops(board);
            PopulateBoardWithQueens(board);
            PopulateBoardWithKings(board);

            return board;
        }


        private static void PopulateBoardWithKings(Board board)
        {
            King king = core.CreateKing(PieceColor.White);
            board.PutAt(king, core.CreatePosition(0, 3));
            king = core.CreateKing(PieceColor.Black);
            board.PutAt(king, core.CreatePosition(7, 3));
        }

        private static void PopulateBoardWithQueens(Board board)
        {
            Queen queen = core.CreateQueen(PieceColor.White);
            board.PutAt(queen, core.CreatePosition(0, 4));
            queen = core.CreateQueen(PieceColor.Black);
            board.PutAt(queen, core.CreatePosition(7, 4));
        }

        private static void PopulateBoardWithBishops(Board board)
        {
            Bishop bishop = core.CreateBishop(PieceColor.White);
            board.PutAt(bishop, core.CreatePosition(0, 5));
            Cloner cloner = core.CreateCloner();
            bishop = cloner.Clone<Bishop>(bishop);
            board.PutAt(bishop, core.CreatePosition(0, 2));
            bishop = core.CreateBishop(PieceColor.Black);
            board.PutAt(bishop, core.CreatePosition(7, 5));
            bishop = cloner.Clone<Bishop>(bishop);
            board.PutAt(bishop, core.CreatePosition(7, 2));
        }

        private static void PopulateBoardWithPawns(Board board)
        {
            Piece piece;
            PieceColor pieceColor;
            int pawnLine;
            int column;
            for (int i = 0; i < 16; i++)
            {
                pieceColor = (i < 8) ? PieceColor.White : PieceColor.Black;
                pawnLine = (i < 8) ? 1 : 6;
                column = (i < 8) ? i : 15 - i;
                piece = core.CreatePawn(pieceColor);
                board.PutAt(piece, core.CreatePosition(pawnLine, column));
            }
        }

        private static void PopulateBoardWithKnights(Board board)
        {
            Knight knight = core.CreateKnight(PieceColor.White);
            board.PutAt(knight, core.CreatePosition(0, 6));
            Cloner cloner = core.CreateCloner();
            knight = cloner.Clone<Knight>(knight);
            board.PutAt(knight, core.CreatePosition(0, 1));
            knight = core.CreateKnight(PieceColor.Black);
            board.PutAt(knight, core.CreatePosition(7, 6));
            knight = cloner.Clone<Knight>(knight);
            board.PutAt(knight, core.CreatePosition(7, 1));
        }

        private static void PopulateBoardWithRooks(Board board)
        {
            Rook rook = core.CreateRook(PieceColor.White);
            board.PutAt(rook, core.CreatePosition(0, 7));
            Cloner cloner = core.CreateCloner();
            rook = cloner.Clone<Rook>(rook);
            board.PutAt(rook, core.CreatePosition(0, 0));
            rook = core.CreateRook(PieceColor.Black);
            board.PutAt(rook, core.CreatePosition(7, 7));
            rook = cloner.Clone<Rook>(rook);
            board.PutAt(rook, core.CreatePosition(7, 0));
        }
    }
}
