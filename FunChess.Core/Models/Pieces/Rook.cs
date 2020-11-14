using FunChess.Core.Enums;
using FunChess.Core.Factory;

namespace FunChess.Core.Models.Pieces
{
    public class Rook : Piece
    {
        public Rook() { }
        public Rook(CoreFactory core, PieceColor pieceColor)
            : base(core, pieceColor)
        {
        }

        public override bool[,] GetPermissionMatrix(Board board)
        {
            throw new System.NotImplementedException();
        }
    }
}
