using System;

namespace FunChess.Core.Models
{
    public class Position
    {
        public int Line { get; set; }
        public int Column { get; set; }

        public Position() { }
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

        public override string ToString()
        {
            return ConvertToChessSymbology(this);
        }

        public string ConvertToChessSymbology(Position position)
        {
            string letter = ConvertToLetter(position.Column);
            string digit = (position.Line + 1).ToString();
            return string.Concat(letter, digit);
        }


        private string ConvertToLetter(int column)
        {
            return column switch
            {
                0 => "A",
                1 => "B",
                2 => "C",
                3 => "D",
                4 => "E",
                5 => "F",
                6 => "G",
                7 => "H",
                _ => "Erro"
            };
        }
    }
}
