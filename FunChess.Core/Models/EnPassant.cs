using FunChess.Core.Enums;

namespace FunChess.Core.Models
{
    public class EnPassant
    {
        public EnPassantSide Side { get; set; }

        public EnPassant(EnPassantSide side)
        {
            Side = side;
        }
    }
}
