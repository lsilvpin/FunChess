using FunChess.Core.Enums;
using FunChess.Core.Factory;
using FunChess.Core.Models;
using FunChess.Core.Models.DTOs;
using FunChess.Core.Tools;
using System.Text.RegularExpressions;
using static FunChess.Core.Models.DTOs.RookPermissionCheckModel;

namespace FunChess.Core.BusinessLogic
{
    public class Brain
    {
        private readonly CoreFactory core;

        public Brain(CoreFactory core)
        {
            this.core = core;
        }


        public Position ConvertSquareSignToPosition(string squareName)
        {
            Regex regex = new Regex("([a-h])([1-8])", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Match match = regex.Match(squareName);

            if (match.Success)
            {
                int line = PrvConvertLetterToLine(match.Groups[1].Value.ToLower());
                int column = PrvConvertNumberToColumn(match.Groups[2].Value);
                return core.CreatePosition(line, column);
            }

            return null;
        }

        public void CheckPositionsToCalculateBishopPermissionMatrix(Board board, Orientation orientation, 
            Position origin, bool[,] permissionMatrix, PieceColor color)
        {
            Position position;
            Piece piece;
            bool isBreak;

            for (int i = 1; i < 8; i++)
            {
                position = PrvGetCheckingPosition(orientation, i, origin);

                if (Check.IsInsideLimits(position))
                {
                    piece = board.LookAt(position);
                    isBreak = PrvValidatePiece(piece, position, permissionMatrix, color);
                    if (isBreak) { break; }
                }
                else
                {
                    break;
                }
            }
        }

        public void CheckPositionsToCalculateRookPermissionMatrix(Board board, Orientation orientation, Position origin, 
            bool[,] permissionMatrix, PieceColor color)
        {
            RookPermissionCheckModel checkModel = new RookPermissionCheckModel();

            checkModel.IsPositiveOrientation = PrvGetOrientationSign(orientation);
            checkModel.Direction = PrvGetDirection(orientation);

            checkModel.StartIndex = checkModel.IsPositiveOrientation ? 1 : -1;
            checkModel.EndIndex = checkModel.IsPositiveOrientation ? 8 : -8;

            checkModel.LoopCondition = PrvGetLoopCondition(checkModel.IsPositiveOrientation);

            PrvCheckPositionsToCalculateTookPermissionMatrix(checkModel, board, origin, permissionMatrix, color);
        }


        #region Private helpers
        private void PrvCheckPositionsToCalculateTookPermissionMatrix(
            RookPermissionCheckModel checkModel, Board board, Position origin, bool[,] permissionMatrix, PieceColor color)
        {
            Position position;
            bool isBreak;

            for (int increment = checkModel.StartIndex;
                checkModel.LoopCondition.Invoke(increment, checkModel.EndIndex);
                PrvIncrement(checkModel.IsPositiveOrientation, ref increment))
            {
                position = PrvGetPositionFromDirection(checkModel.Direction, increment, origin);

                if (Check.IsInsideLimits(checkModel.Direction, position))
                {
                    isBreak = PrvValidatePosition(position, board, permissionMatrix, color);
                    if (isBreak) { break; }
                }
                else
                {
                    break;
                }
            }
        }

        private bool PrvValidatePosition(Position position, Board board, bool[,] permissionMatrix, PieceColor color)
        {
            Piece piece = board.LookAt(position);

            if (piece == null)
            {
                permissionMatrix[position.Line, position.Column] = true;
                return false;
            }
            else if (piece.Color != color)
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

        private Position PrvGetPositionFromDirection(Direction direction, int increment, Position position)
        {
            if (direction == Direction.Vertical)
            {
                return core.CreatePosition(position.Line + increment, position.Column);
            }
            else
            {
                return core.CreatePosition(position.Line, position.Column + increment);
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

        private bool PrvValidatePiece(Piece piece, Position position, bool[,] permissionMatrix, PieceColor color)
        {
            if (piece == null)
            {
                permissionMatrix[position.Line, position.Column] = true;
                return false;
            }
            else if (piece.Color != color)
            {
                permissionMatrix[position.Line, position.Column] = true;
                return true;
            }
            else
            {
                return true;
            }
        }

        private Position PrvGetCheckingPosition(Orientation orientation, int increment, Position origin)
        {
            if (orientation == Orientation.NorthEast)
            {
                return core.CreatePosition(origin.Line + increment, origin.Column + increment);
            }
            else if (orientation == Orientation.SouthEast)
            {
                return core.CreatePosition(origin.Line - increment, origin.Column + increment);
            }
            else if (orientation == Orientation.SouthWest)
            {
                return core.CreatePosition(origin.Line - increment, origin.Column - increment);
            }
            else
            {
                return core.CreatePosition(origin.Line + increment, origin.Column - increment);
            }
        }

        private int PrvConvertNumberToColumn(string value)
        {
            int.TryParse(value, out int column);
            return --column;
        }

        private int PrvConvertLetterToLine(string letter)
        {
            return letter switch
            {
                "a" => 0,
                "b" => 1,
                "c" => 2,
                "d" => 3,
                "e" => 4,
                "f" => 5,
                "g" => 6,
                _ => 7
            };
        }
        #endregion
    }
}
