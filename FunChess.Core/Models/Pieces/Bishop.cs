using FunChess.Core.Enums;
using FunChess.Core.Factory;
using FunChess.Core.Tools;
using System;

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
            PrvCheckDirectionIntoOrientation(board, Orientation.NorthEast);
            PrvCheckDirectionIntoOrientation(board, Orientation.SouthEast);
            PrvCheckDirectionIntoOrientation(board, Orientation.SouthWest);
            PrvCheckDirectionIntoOrientation(board, Orientation.NorthWest);

            return permissionMatrix;
        }


        #region Private helpers
        private void PrvCheckDirectionIntoOrientation(Board board, Orientation orientation)
        {
            Position position;
            Piece piece;
            bool isBreak;

            for (int i = 1; i < 8; i++)
            {
                position = PrvGetCheckingPosition(orientation, i);

                if (Check.IsInsideLimits(position))
                {
                    piece = board.LookAt(position);
                    isBreak = PrvValidatePiece(piece, position);
                    if (isBreak) { break; }
                }
                else
                {
                    break;
                }
            }
        }

        private bool PrvValidatePiece(Piece piece, Position position)
        {
            if (piece == null)
            {
                permissionMatrix[position.Line, position.Column] = true;
                return false;
            }
            else if (piece.Color != Color)
            {
                permissionMatrix[position.Line, position.Column] = true;
                return true;
            }
            else
            {
                return true;
            }
        }

        private Position PrvGetCheckingPosition(Orientation orientation, int increment)
        {
            if (orientation == Orientation.NorthEast)
            {
                return core.CreatePosition(Position.Line + increment, Position.Column + increment);
            }
            else if (orientation == Orientation.SouthEast)
            {
                return core.CreatePosition(Position.Line - increment, Position.Column + increment);
            }
            else if (orientation == Orientation.SouthWest)
            {
                return core.CreatePosition(Position.Line - increment, Position.Column - increment);
            }
            else
            {
                return core.CreatePosition(Position.Line + increment, Position.Column - increment);
            }
        }
        #endregion
    }
}
