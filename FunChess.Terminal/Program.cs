using FunChess.Core.Models;
using FunChess.Core.Tools;

namespace FunChess.Terminal
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Board board = Sample.CreateDefaultBoard();
            board.PrintInConsole();
        }
    }
}
