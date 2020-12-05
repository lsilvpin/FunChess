using FunChess.Core.BusinessLogic;
using FunChess.Core.Enums;
using FunChess.Core.Factory;

namespace FunChess.Core.Models.Pieces
{
    public class Bishop : Piece
    {
        private readonly Brain brain;

        public Bishop() : base(Beyond.Core) { }
        public Bishop(CoreFactory core, PieceColor pieceColor, Brain brain)
            : base(core, pieceColor)
        {
            this.brain = brain;
        }


        public override bool[,] GetPermissionMatrix(Board board)
        {
            brain.CheckPositionsToCalculateBishopPermissionMatrix(board, Orientation.NorthEast, Position, permissionMatrix, Color);
            brain.CheckPositionsToCalculateBishopPermissionMatrix(board, Orientation.SouthEast, Position, permissionMatrix, Color);
            brain.CheckPositionsToCalculateBishopPermissionMatrix(board, Orientation.SouthWest, Position, permissionMatrix, Color);
            brain.CheckPositionsToCalculateBishopPermissionMatrix(board, Orientation.NorthWest, Position, permissionMatrix, Color);

            return permissionMatrix;
        }

        public override string ToString()
        {
            return "Bi";
        }
    }
}
