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
                int line = ConvertLetterToLine(match.Groups[1].Value.ToLower());
                int column = ConvertNumberToColumn(match.Groups[2].Value);
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
                position = GetCheckingPosition(orientation, i, origin);

                if (Check.IsInsideLimits(position))
                {
                    piece = board.LookAt(position);
                    isBreak = ValidatePiece(piece, position, permissionMatrix, color);
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

            checkModel.IsPositiveOrientation = GetOrientationSign(orientation);
            checkModel.Direction = GetDirection(orientation);

            checkModel.StartIndex = checkModel.IsPositiveOrientation ? 1 : -1;
            checkModel.EndIndex = checkModel.IsPositiveOrientation ? 8 : -8;

            checkModel.LoopCondition = GetLoopCondition(checkModel.IsPositiveOrientation);

            CheckPositionsToCalculateTookPermissionMatrix(checkModel, board, origin, permissionMatrix, color);
        }


        private void CheckPositionsToCalculateTookPermissionMatrix(
            RookPermissionCheckModel checkModel, Board board, Position origin, bool[,] permissionMatrix, PieceColor color)
        {
            Position position;
            bool isBreak;

            for (int increment = checkModel.StartIndex;
                checkModel.LoopCondition.Invoke(increment, checkModel.EndIndex);
                Increment(checkModel.IsPositiveOrientation, ref increment))
            {
                position = GetPositionFromDirection(checkModel.Direction, increment, origin);

                if (Check.IsInsideLimits(checkModel.Direction, position))
                {
                    isBreak = ValidatePosition(position, board, permissionMatrix, color);
                    if (isBreak) { break; }
                }
                else
                {
                    break;
                }
            }
        }

        private bool ValidatePosition(Position position, Board board, bool[,] permissionMatrix, PieceColor color)
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

        private loopCondition GetLoopCondition(bool isPositiveOrientation)
        {
            loopCondition loopCondition;

            if (isPositiveOrientation)
            {
                loopCondition = LessThan;
            }
            else
            {
                loopCondition = GreaterThan;
            }

            return loopCondition;
        }

        private Position GetPositionFromDirection(Direction direction, int increment, Position position)
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

        private Direction GetDirection(Orientation orientation)
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

        private bool GetOrientationSign(Orientation direction)
        {
            if (direction == Orientation.North || direction == Orientation.East)
            {
                return true;
            }

            return false;
        }

        private bool LessThan(int first, int second)
        {
            return first < second;
        }

        private bool GreaterThan(int first, int second)
        {
            return first > second;
        }

        private void Increment(bool isPositiveOrientation, ref int line)
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

        private bool ValidatePiece(Piece piece, Position position, bool[,] permissionMatrix, PieceColor color)
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

        private Position GetCheckingPosition(Orientation orientation, int increment, Position origin)
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

        private int ConvertNumberToColumn(string value)
        {
            int.TryParse(value, out int column);
            return --column;
        }

        private int ConvertLetterToLine(string letter)
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
    }
}
