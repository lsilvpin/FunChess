using FunChess.Core.Exceptions;
using FunChess.Core.Models;
using System;

namespace FunChess.Core.Tools
{
    public static class Validate
    {
        private const string PositionOutsideLimits = "The position is outside the limits.";
        private const string ShouldNotBeZero = "The integer should not be zero.";

        public static void IsInsideLimits(Position position)
        {
            IsInsideLimits(position.Line, position.Column);
        }

        internal static void IsInsideLimits(int line, int column)
        {
            if (line < 0 || line > 7 || column < 0 || column > 7)
            {
                throw new AssertException(PositionOutsideLimits);
            }
        }

        internal static void IsNotZero(int integer)
        {
            if (integer == 0)
            {
                throw new AssertException(ShouldNotBeZero);
            }
        }
    }
}
