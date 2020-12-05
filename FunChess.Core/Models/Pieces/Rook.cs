using FunChess.Core.BusinessLogic;
using FunChess.Core.Enums;
using FunChess.Core.Factory;

namespace FunChess.Core.Models.Pieces
{
    public class Rook : Piece
    {
        private readonly Brain brain;

        public Rook() : base(Beyond.Core) { }
        public Rook(CoreFactory core, PieceColor pieceColor, Brain brain)
            : base(core, pieceColor)
        {
            this.brain = brain;
        }


        public override bool[,] GetPermissionMatrix(Board board)
        {
            brain.CheckPositionsToCalculateRookPermissionMatrix(board, Orientation.North, Position, permissionMatrix, Color);
            brain.CheckPositionsToCalculateRookPermissionMatrix(board, Orientation.East, Position, permissionMatrix, Color);
            brain.CheckPositionsToCalculateRookPermissionMatrix(board, Orientation.South, Position, permissionMatrix, Color);
            brain.CheckPositionsToCalculateRookPermissionMatrix(board, Orientation.West, Position, permissionMatrix, Color);

            return permissionMatrix;
        }

        public override string ToString()
        {
            return "Ro";
        }
    }
}
