using FunChess.Core.Factory;
using FunChess.Core.Models;
using System;
using System.Text.RegularExpressions;

namespace FunChess.Core.BusinessLogic
{
    public class Brain
    {
        private readonly CoreFactory core;

        public Brain(CoreFactory core)
        {
            this.core = core;
        }

        public Position ConvertSquareNameToCoordinate(string squareName)
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

        #region Private helpers

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
