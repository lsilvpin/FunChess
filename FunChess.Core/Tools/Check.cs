using FunChess.Core.Enums;
using FunChess.Core.Models;

namespace FunChess.Core.Tools
{
    public static class Check
    {
        public static bool IsInsideLimits(Direction direction, Position position)
        {
            if (direction == Direction.Vertical)
            {
                return IsInsideLimits(position.Line);
            }
            else
            {
                return IsInsideLimits(position.Column);
            }
        }

        public static bool IsInsideLimits(int coordinate)
        {
            if (coordinate < 0 || coordinate > 7)
            {
                return false;
            }

            return true;
        }

        public static bool IsInsideLimits(Position position)
        {
            return IsInsideLimits(position.Line, position.Column);
        }

        public static bool IsInsideLimits(int line, int column)
        {
            if (line < 0 || line > 7 || column < 0 || column > 7)
            {
                return false;
            }

            return true;
        }
    }
}
