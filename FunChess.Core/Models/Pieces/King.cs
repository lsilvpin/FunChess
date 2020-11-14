using FunChess.Core.Enums;
using FunChess.Core.Factory;
using System;

namespace FunChess.Core.Models.Pieces
{
    public class King : Piece
    {
        public King() { }
        public King(CoreFactory core, PieceColor pieceColor)
            : base(core, pieceColor)
        {
        }

        public override bool[,] GetPermissionMatrix(Board board)
        {
            throw new NotImplementedException();
        }
    }
}
