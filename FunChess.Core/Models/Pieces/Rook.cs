using FunChess.Core.Enums;
using FunChess.Core.Factory;
using FunChess.Core.Tools;
using System;
using static FunChess.Core.Models.Pieces.RookPermissionCheckModel;

namespace FunChess.Core.Models.Pieces
{
    public class Rook : Piece
    {
        public Rook() : base(Beyond.Core) { }
        public Rook(CoreFactory core, PieceColor pieceColor)
            : base(core, pieceColor)
        {
        }


        public override bool[,] GetPermissionMatrix(Board board)
        {
            PrvInvestigateDirectionIntoOrientation(board, Orientation.North);
            PrvInvestigateDirectionIntoOrientation(board, Orientation.East);
            PrvInvestigateDirectionIntoOrientation(board, Orientation.South);
            PrvInvestigateDirectionIntoOrientation(board, Orientation.West);

            return permissionMatrix;
        }


        #region Private helpers
        private void PrvInvestigateDirectionIntoOrientation(Board board, Orientation orientation)
        {
            RookPermissionCheckModel checkModel = new RookPermissionCheckModel();

            checkModel.IsPositiveOrientation = PrvGetOrientationSign(orientation);
            checkModel.Direction = PrvGetDirection(orientation);

            checkModel.StartIndex = checkModel.IsPositiveOrientation ? 1 : -1;
            checkModel.EndIndex = checkModel.IsPositiveOrientation ? 8 : -8;

            checkModel.LoopCondition = PrvGetLoopCondition(checkModel.IsPositiveOrientation);

            PrvCheckPositionsInCurrentDirectionAndOrientation(checkModel, board);
        }

        private void PrvCheckPositionsInCurrentDirectionAndOrientation(RookPermissionCheckModel checkModel, Board board)
        {
            Position position;
            bool isBreak;

            for (int increment = checkModel.StartIndex; 
                checkModel.LoopCondition.Invoke(increment, checkModel.EndIndex); 
                PrvIncrement(checkModel.IsPositiveOrientation, ref increment))
            {
                position = PrvGetPositionFromDirection(checkModel.Direction, increment);

                if (Check.IsInsideLimits(checkModel.Direction, position))
                {
                    isBreak = PrvValidatePosition(position, board);
                    if (isBreak) { break; }
                }
                else
                {
                    break;
                }
            }
        }

        private bool PrvValidatePosition(Position position, Board board)
        {
            Piece piece = board.LookAt(position);

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

        private loopCondition PrvGetLoopCondition(bool isPositiveOrientation)
        {
            loopCondition loopCondition;

            if (isPositiveOrientation)
            {
                loopCondition = PrvLessThan;
            }
            else
            {
                loopCondition = PrvGreaterThan;
            }

            return loopCondition;
        }

        private Position PrvGetPositionFromDirection(Direction direction, int increment)
        {
            if (direction == Direction.Vertical)
            {
                return core.CreatePosition(Position.Line + increment, Position.Column);
            }
            else
            {
                return core.CreatePosition(Position.Line, Position.Column + increment);
            }
        }

        private Direction PrvGetDirection(Orientation orientation)
        {
            if (orientation == Orientation.North || orientation == Orientation.South)
            {
                return Direction.Vertical;
            }
            else
            {
                return Direction.Horizontal;
            }
        }

        private bool PrvGetOrientationSign(Orientation direction)
        {
            if (direction == Orientation.North || direction == Orientation.East)
            {
                return true;
            }

            return false;
        }

        private bool PrvLessThan(int first, int second)
        {
            return first < second;
        }

        private bool PrvGreaterThan(int first, int second)
        {
            return first > second;
        }

        private void PrvIncrement(bool isPositiveOrientation, ref int line)
        {
            if (isPositiveOrientation)
            {
                line++;
            }
            else
            {
                line--;
            }
        }
        #endregion
    }
}
