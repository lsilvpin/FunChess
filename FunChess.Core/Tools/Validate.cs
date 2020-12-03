using FunChess.Core.Exceptions;
using FunChess.Core.Models;

namespace FunChess.Core.Tools
{
    public static class Validate
    {
        private const string PositionOutsideLimits = "The position is outside the limits.";

        public static void IsInsideLimits(Position position)
        {
            IsInsideLimits(position.Line, position.Column);
        }

        public static void IsInsideLimits(int line, int column)
        {
            if (line < 0 || line > 7 || column < 0 || column > 7)
            {
                throw new AssertException(PositionOutsideLimits);
            }
        }
    }
}
