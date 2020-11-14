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

        public void PutAt(Piece piece, Position position)
        {
            Grid[position.Line, position.Column] = piece;
            piece.Position = position;
        }

        public Piece TakeAt(Position position)
        {
            Piece piece = Grid[position.Line, position.Column];
            Grid[position.Line, position.Column] = null;
            return piece;
        }

        public Piece LookAt(Position position)
        {
            return Grid[position.Line, position.Column];
        }
    }
}
