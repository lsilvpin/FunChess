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
            CheckHorizontally(permissionMatrix, board);
            CheckVertically(permissionMatrix, board);

            return permissionMatrix;
        }

        public override string ToString()
        {
            return "Kn";
        }


        private void CheckVertically(bool[,] permissionMatrix, Board board)
        {
            int line = currentPosition.Line + one;
            int column = currentPosition.Column + two;

            UpdatePermissionMatrix(permissionMatrix, line, column, board);

            column = currentPosition.Column - two;

            UpdatePermissionMatrix(permissionMatrix, line, column, board);

            line = currentPosition.Line - one;

            UpdatePermissionMatrix(permissionMatrix, line, column, board);

            column = currentPosition.Column + two;

            UpdatePermissionMatrix(permissionMatrix, line, column, board);
        }

        private void CheckHorizontally(bool[,] permissionMatrix, Board board)
        {
            int line = currentPosition.Line + two;
            int column = currentPosition.Column + one;

            UpdatePermissionMatrix(permissionMatrix, line, column, board);

            column = currentPosition.Column - one;

            UpdatePermissionMatrix(permissionMatrix, line, column, board);

            line = currentPosition.Line - two;

            UpdatePermissionMatrix(permissionMatrix, line, column, board);

            column = currentPosition.Column + one;

            UpdatePermissionMatrix(permissionMatrix, line, column, board);
        }

        private void UpdatePermissionMatrix(bool[,] permissionMatrix, int line, int column, Board board)
        {
            Piece pieceAtPosition = board.LookAt(core.CreatePosition(line, column));

            if (Check.IsInsideLimits(line, column)
                && (pieceAtPosition == null || pieceAtPosition.Color != Color))
            {
                permissionMatrix[line, column] = true;
            }
        }
    }
}
