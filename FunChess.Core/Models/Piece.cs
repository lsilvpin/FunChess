using FunChess.Core.Enums;
using System;

namespace FunChess.Core.Models
{
    public abstract class Piece
    {
        public PieceColor Color { get; set; }

        protected Piece(PieceColor color)
        {
            Color = color;
        }

        public override bool Equals(object obj)
        {
            return obj is Piece piece &&
                   Color == piece.Color;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Color);
        }
    }
}
