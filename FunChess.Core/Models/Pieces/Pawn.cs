using FunChess.Core.Enums;
using FunChess.Core.Exceptions;
using FunChess.Core.Factory;
using System;

namespace FunChess.Core.Models.Pieces
{
    public class Pawn : Piece
    {
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

            PrvValidateNextSquare(increment, board.Grid, permissionMatrix);
            PrvValidateThePresenceOfLeftFoe(increment, board.Grid, permissionMatrix);
            PrvValidateThePresenceOfRightFoe(increment, board.Grid, permissionMatrix);
            PrvValidateSecondNextSquareInCasePawnIsAtInitialLine(increment, initialLine, board.Grid, permissionMatrix);

            return permissionMatrix;
        }

        #region Private helpers
        #region GetPermissionMatrix helpers
        private void PrvValidateSecondNextSquareInCasePawnIsAtInitialLine(int increment, int initialLine, Piece[,] grid, bool[,] permissionMatrix)
        {
            Position position = core.CreatePosition(Position.Line + 2 * increment, Position.Column);

            if (PrvIsInitialtLine(Position.Line, initialLine) && PrvIsEmptySquare(grid, position))
            {
                PrvApprovePosition(permissionMatrix, position);
            }
        }

        private bool PrvIsInitialtLine(int line, int initialLine)
        {
            return line == initialLine;
        }

        private void PrvValidateThePresenceOfRightFoe(int increment, Piece[,] grid, bool[,] permissionMatrix)
        {
            Position position = core.CreatePosition(Position.Line + increment, Position.Column + increment);

            if (PrvIsInsideLimits(position.Column) && PrvIsThereAnyFoeAt(grid, position))
            {
                PrvApprovePosition(permissionMatrix, position);
            }
        }

        private void PrvValidateThePresenceOfLeftFoe(int increment, Piece[,] grid, bool[,] permissionMatrix)
        {
            Position position = core.CreatePosition(Position.Line + increment, Position.Column - increment);

            if (PrvIsInsideLimits(position.Column) && PrvIsThereAnyFoeAt(grid, position))
            {
                PrvApprovePosition(permissionMatrix, position);
            }
        }

        private bool PrvIsThereAnyFoeAt(Piece[,] grid, Position position)
        {
            Piece piece = grid[position.Line, position.Column];
            return piece != null && piece.Color != Color;
        }

        private void PrvValidateNextSquare(int increment, Piece[,] grid, bool[,] permissionMatrix)
        {
            Position position = core.CreatePosition(Position.Line + increment, Position.Column);

            if (PrvIsInsideLimits(position.Line) && PrvIsEmptySquare(grid, position))
            {
                PrvApprovePosition(permissionMatrix, position);
            }
        }

        private bool PrvIsEmptySquare(Piece[,] grid, Position position)
        {
            return grid[position.Line, position.Column] == null;
        }

        private void PrvApprovePosition(bool[,] permissionMatrix, Position position)
        {
            permissionMatrix[position.Line, position.Column] = true;
        }

        private bool PrvIsInsideLimits(int coordinate)
        {
            return 0 <= coordinate && coordinate <= 7;
        }
        #endregion
        #endregion
    }
}
