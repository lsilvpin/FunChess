using FunChess.Core.Enums;
using FunChess.Core.Factory;
using FunChess.Core.Tools;

namespace FunChess.Core.Models.Pieces
{
    public class King : Piece
    {
        public King() : base(Beyond.Core) { }
        public King(CoreFactory core, PieceColor pieceColor)
            : base(core, pieceColor)
        {
        }


        public override bool[,] GetPermissionMatrix(Board board)
        {
            Position position = core.CreatePosition(Position.Line + 1, Position.Column);

            ValidatePosition(board, position);
            StepRight(board, position);
            StepDown(board, position);
            StepDown(board, position);
            StepLeft(board, position);
            StepLeft(board, position);
            StepUp(board, position);
            StepUp(board, position);

            return permissionMatrix;
        }

        public override string ToString()
        {
            return "Ki";
        }


        private void StepRight(Board board, Position position)
        {
            StepRight(position);
            ValidatePosition(board, position);
        }

        private void StepRight(Position position)
        {
            position.Column = position.Column + 1;
        }

        private void StepDown(Board board, Position position)
        {
            StepDown(position);
            ValidatePosition(board, position);
        }

        private void StepDown(Position position)
        {
            position.Line = position.Line - 1;
        }

        private void StepLeft(Board board, Position position)
        {
            StepLeft(position);
            ValidatePosition(board, position);
        }

        private void StepLeft(Position position)
        {
            position.Column = position.Column - 1;
        }

        private void StepUp(Board board, Position position)
        {
            StepUp(position);
            ValidatePosition(board, position);
        }

        private void StepUp(Position position)
        {
            position.Line = position.Line + 1;
        }

        private void ValidatePosition(Board board, Position position)
        {
            if (Check.IsInsideLimits(position))
            {
                Piece piece = board.LookAt(position);

                if (IsEmpty(piece))
                {
                    ApprovePosition(position);
                }
                else if (IsFoe(piece))
                {
                    ApprovePosition(position);
                }
            }
        }

        private bool IsEmpty(Piece piece)
        {
            return piece == null;
        }

        private bool IsFoe(Piece piece)
        {
            return piece.Color != Color;
        }

        private void ApprovePosition(Position position)
        {
            permissionMatrix[position.Line, position.Column] = true;
        }
    }
}
