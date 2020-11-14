using System;

namespace FunChess.Core.Models
{
    public class Position
    {
        public int Line { get; set; }
        public int Column { get; set; }

        public Position(int line, int column)
        {
            Line = line;
            Column = column;
        }

        public override bool Equals(object obj)
        {
            return obj is Position coordinate &&
                   Line == coordinate.Line &&
                   Column == coordinate.Column;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Line, Column);
        }
    }
}
