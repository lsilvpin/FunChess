using FunChess.Core.Models;

namespace FunChess.Core.Tools
{
    public static class Check
    {
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
