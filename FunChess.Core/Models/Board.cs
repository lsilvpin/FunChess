using System;

namespace FunChess.Core.Models
{
    public class Board
    {
        public Piece[,] Grid { get; set; }

        public Board()
        {
            Grid = new Piece[8, 8];
        }

        public void PutAt(Piece piece, int line, int column)
        {
            Grid[line, column] = piece;
        }

        public Piece TakeAt(int line, int column)
        {
            Piece piece = Grid[line, column];
            Grid[line, column] = null;
            return piece;
        }
    }
}
