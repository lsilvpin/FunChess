using FunChess.Core.Enums;
using FunChess.Core.Exceptions;
using FunChess.Core.Factory;

namespace FunChess.Core.Models.Pieces
{
    public class Pawn : Piece
    {
        public Pawn() : base(Beyond.Core) { }
        public Pawn(CoreFactory core, PieceColor pieceColor)
            : base(core, pieceColor)
        {
        }


        public override bool[,] GetPermissionMatrix(Board board)
        {
            if (Position == null)
                throw new PieceOutOfBoardException(GetType(), Color);

            int increment = (Color == PieceColor.White) ? +1 : -1;
            int initialLine = (Color == PieceColor.White) ? 1 : 6;
            int enPassantLine = (Color == PieceColor.White) ? 4 : 3;

            ValidateNextSquare(increment, board.Grid, permissionMatrix);
            ValidateThePresenceOfLeftFoe(increment, board.Grid, permissionMatrix);
            ValidateThePresenceOfRightFoe(increment, board.Grid, permissionMatrix);
            ValidateSecondNextSquareInCasePawnIsAtInitialLine(increment, initialLine, board.Grid, permissionMatrix);
            ValidateEnPassant(board, enPassantLine, increment, permissionMatrix);

            return permissionMatrix;
        }

        public override string ToString()
        {
            return "Pa";
        }


        private void ValidateEnPassant(Board board, int enPassantLine, int increment, bool[,] permissionMatrix)
        {
            if (IsEnPassant(board, enPassantLine))
            {
                int permitedLine = enPassantLine + increment;
                if (board.EnPassant.Side == EnPassantSide.Left)
                {
                    ApprovePosition(permissionMatrix, core.CreatePosition(permitedLine, Position.Column - 1));
                }
                else // EnPassantSide.Right
                {
                    ApprovePosition(permissionMatrix, core.CreatePosition(permitedLine, Position.Column + 1));
                }
            }
        }

        private bool IsEnPassant(Board board, int enPassantLine)
        {
            return Position.Line == enPassantLine && board.EnPassant != null;
        }

        private void ValidateSecondNextSquareInCasePawnIsAtInitialLine(int increment, int initialLine, Piece[,] grid, bool[,] permissionMatrix)
        {
            Position position = core.CreatePosition(Position.Line + 2 * increment, Position.Column);

            if (IsInitialtLine(Position.Line, initialLine) && IsEmptySquare(grid, position))
            {
                ApprovePosition(permissionMatrix, position);
            }
        }

        private bool IsInitialtLine(int line, int initialLine)
        {
            return line == initialLine;
        }

        private void ValidateThePresenceOfRightFoe(int increment, Piece[,] grid, bool[,] permissionMatrix)
        {
            Position position = core.CreatePosition(Position.Line + increment, Position.Column + increment);

            if (IsInsideLimits(position.Column) && IsThereAnyFoeAt(grid, position))
            {
                ApprovePosition(permissionMatrix, position);
            }
        }

        private void ValidateThePresenceOfLeftFoe(int increment, Piece[,] grid, bool[,] permissionMatrix)
        {
            Position position = core.CreatePosition(Position.Line + increment, Position.Column - increment);

            if (IsInsideLimits(position.Column) && IsThereAnyFoeAt(grid, position))
            {
                ApprovePosition(permissionMatrix, position);
            }
        }

        private bool IsThereAnyFoeAt(Piece[,] grid, Position position)
        {
            Piece piece = grid[position.Line, position.Column];
            return piece != null && piece.Color != Color;
        }

        private void ValidateNextSquare(int increment, Piece[,] grid, bool[,] permissionMatrix)
        {
            Position position = core.CreatePosition(Position.Line + increment, Position.Column);

            if (IsInsideLimits(position.Line) && IsEmptySquare(grid, position))
            {
                ApprovePosition(permissionMatrix, position);
            }
        }

        private bool IsEmptySquare(Piece[,] grid, Position position)
        {
            return grid[position.Line, position.Column] == null;
        }

        private bool IsInsideLimits(int coordinate)
        {
            return 0 <= coordinate && coordinate <= 7;
        }
    }
}
