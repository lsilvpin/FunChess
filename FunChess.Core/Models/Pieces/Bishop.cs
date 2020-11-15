using FunChess.Core.Enums;
using FunChess.Core.Factory;

namespace FunChess.Core.Models.Pieces
{
    public class Bishop : Piece
    {
        public Bishop() : base(Beyond.Core) { }
        public Bishop(CoreFactory core, PieceColor pieceColor)
            : base(core, pieceColor)
        {
        }

        public override bool[,] GetPermissionMatrix(Board board)
        {
            PrvValidateNorthEast(board);
            PrvValidateNorthWest(board);
            PrvValidateSouthWest(board);
            PrvValidateSouthEast(board);

            return permissionMatrix;
        }

        #region Private helpers
        private void PrvValidateSouthEast(Board board)
        {
            int i = 0;
            Piece piece;
            Position position = core.CreatePosition(Position.Line - i, Position.Column + i);
            while (1 <= position.Line && position.Column <= 6)
            {
                i++;
                PrvMovePositionOneStep(position, -i, +i);
                piece = board.LookAt(position);
                if (PrvIsBishopBlocked(piece))
                    break;
                if (PrvIsFreeWay(piece))
                    ApprovePosition(permissionMatrix, position);
                if (PrvIsBishopThreatening(piece))
                {
                    ApprovePosition(permissionMatrix, position);
                    break;
                }
            }
        }

        private void PrvValidateSouthWest(Board board)
        {
            int i = 0;
            Piece piece;
            Position position = core.CreatePosition(Position.Line - i, Position.Column - i);
            while (1 <= position.Line)
            {
                i++;
                PrvMovePositionOneStep(position, -i, -i);
                piece = board.LookAt(position);
                if (PrvIsBishopBlocked(piece))
                    break;
                if (PrvIsFreeWay(piece))
                    ApprovePosition(permissionMatrix, position);
                if (PrvIsBishopThreatening(piece))
                {
                    ApprovePosition(permissionMatrix, position);
                    break;
                }
            }
        }

        private void PrvValidateNorthWest(Board board)
        {
            int i = 0;
            Piece piece;
            Position position = core.CreatePosition(Position.Line + i, Position.Column - i);
            while (position.Line <= 6 && 1 <= position.Column)
            {
                i++;
                PrvMovePositionOneStep(position, +i, -i);
                piece = board.LookAt(position);
                if (PrvIsBishopBlocked(piece))
                    break;
                if (PrvIsFreeWay(piece))
                    ApprovePosition(permissionMatrix, position);
                if (PrvIsBishopThreatening(piece))
                {
                    ApprovePosition(permissionMatrix, position);
                    break;
                }
            }
        }

        private void PrvValidateNorthEast(Board board)
        {
            int i = 0;
            Piece piece;
            Position position = core.CreatePosition(Position.Line + i, Position.Column + i);
            while (position.Line <= 6)
            {
                i++;
                PrvMovePositionOneStep(position, +i, +i);
                piece = board.LookAt(position);
                if (PrvIsBishopBlocked(piece))
                    break;
                if (PrvIsFreeWay(piece))
                    ApprovePosition(permissionMatrix, position);
                if (PrvIsBishopThreatening(piece))
                {
                    ApprovePosition(permissionMatrix, position);
                    break;
                }
            }
        }

        private bool PrvIsBishopBlocked(Piece piece)
        {
            return piece != null && piece.Color == Color;
        }

        private bool PrvIsBishopThreatening(Piece piece)
        {
            return piece != null;
        }

        private bool PrvIsFreeWay(Piece piece)
        {
            return piece == null;
        }

        private void PrvMovePositionOneStep(Position position, int a, int b)
        {
            position.Line = Position.Line + a;
            position.Column = Position.Column + b;
        }
        #endregion
    }
}
