using FunChess.Core.Enums;
using FunChess.Core.Factory;
using FunChess.Core.Tools;

namespace FunChess.Core.Models.Pieces
{
    public class Knight : Piece
    {
        private const int one = 1;
        private const int two = 2;
        private Position currentPosition { get { return Position; } }

        public Knight() : base(Beyond.Core) { }
        public Knight(CoreFactory core, PieceColor pieceColor)
            : base(core, pieceColor)
        {
        }

        public override bool[,] GetPermissionMatrix(Board board)
        {
            PrvCheckHorizontally(permissionMatrix, board);
            PrvCheckVertically(permissionMatrix, board);

            return permissionMatrix;
        }

        public override string ToString()
        {
            return "Kn";
        }

        #region Private helpers
        private void PrvCheckVertically(bool[,] permissionMatrix, Board board)
        {
            int line = currentPosition.Line + one;
            int column = currentPosition.Column + two;

            PrvUpdatePermissionMatrix(permissionMatrix, line, column, board);

            column = currentPosition.Column - two;

            PrvUpdatePermissionMatrix(permissionMatrix, line, column, board);

            line = currentPosition.Line - one;

            PrvUpdatePermissionMatrix(permissionMatrix, line, column, board);

            column = currentPosition.Column + two;

            PrvUpdatePermissionMatrix(permissionMatrix, line, column, board);
        }

        private void PrvCheckHorizontally(bool[,] permissionMatrix, Board board)
        {
            int line = currentPosition.Line + two;
            int column = currentPosition.Column + one;

            PrvUpdatePermissionMatrix(permissionMatrix, line, column, board);

            column = currentPosition.Column - one;

            PrvUpdatePermissionMatrix(permissionMatrix, line, column, board);

            line = currentPosition.Line - two;

            PrvUpdatePermissionMatrix(permissionMatrix, line, column, board);

            column = currentPosition.Column + one;

            PrvUpdatePermissionMatrix(permissionMatrix, line, column, board);
        }

        private void PrvUpdatePermissionMatrix(bool[,] permissionMatrix, int line, int column, Board board)
        {
            Piece pieceAtPosition = board.LookAt(core.CreatePosition(line, column));

            if (Check.IsInsideLimits(line, column)
                && (pieceAtPosition == null || pieceAtPosition.Color != Color))
            {
                permissionMatrix[line, column] = true;
            }
        }
        #endregion
    }
}
