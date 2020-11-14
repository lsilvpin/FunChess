using System;

namespace FunChess.Core.Models
{
    public class Coordinate
    {
        public int Line { get; set; }
        public int Column { get; set; }

        public Coordinate(int line, int column)
        {
            Line = line;
            Column = column;
        }

        public override bool Equals(object obj)
        {
            return obj is Coordinate coordinate &&
                   Line == coordinate.Line &&
                   Column == coordinate.Column;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Line, Column);
        }
    }
}
