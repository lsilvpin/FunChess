using FunChess.Core.Enums;
using FunChess.Core.Factory;

namespace FunChess.Core.Models.Pieces
{
    public class Bishop : Piece
    {
        public Bishop() : base(Beyond.Core) { }
        public Bishop(CoreFactory core, PieceColor pieceColor)
            : base(core, pieceColor)
        {
        }

        public override bool[,] GetPermissionMatrix(Board board)
        {
            throw new System.NotImplementedException();
        }
    }
}
