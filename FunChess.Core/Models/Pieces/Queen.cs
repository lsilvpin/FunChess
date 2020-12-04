using FunChess.Core.BusinessLogic;
using FunChess.Core.Enums;
using FunChess.Core.Factory;

namespace FunChess.Core.Models.Pieces
{
    public class Queen : Piece
    {
        private readonly Brain brain;

        public Queen() : base(Beyond.Core) { }
        public Queen(CoreFactory core, PieceColor pieceColor, Brain brain)
            : base(core, pieceColor)
        {
            this.brain = brain;
        }

        public override bool[,] GetPermissionMatrix(Board board)
        {
            brain.CheckPositionsToCalculateRookPermissionMatrix(board, Orientation.North, Position, permissionMatrix, Color);
            brain.CheckPositionsToCalculateBishopPermissionMatrix(board, Orientation.NorthEast, Position, permissionMatrix, Color);
            brain.CheckPositionsToCalculateRookPermissionMatrix(board, Orientation.East, Position, permissionMatrix, Color);
            brain.CheckPositionsToCalculateBishopPermissionMatrix(board, Orientation.SouthEast, Position, permissionMatrix, Color);
            brain.CheckPositionsToCalculateRookPermissionMatrix(board, Orientation.South, Position, permissionMatrix, Color);
            brain.CheckPositionsToCalculateBishopPermissionMatrix(board, Orientation.SouthWest, Position, permissionMatrix, Color);
            brain.CheckPositionsToCalculateRookPermissionMatrix(board, Orientation.West, Position, permissionMatrix, Color);
            brain.CheckPositionsToCalculateBishopPermissionMatrix(board, Orientation.NorthWest, Position, permissionMatrix, Color);

            return permissionMatrix;
        }
    }
}
