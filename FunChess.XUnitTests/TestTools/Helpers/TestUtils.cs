using FunChess.Core.Enums;

namespace FunChess.XUnitTests.TestTools.Helpers
{
    public class TestUtils
    {
        public TestUtils()
        {
        }

        public PieceColor SwitchColor(PieceColor pieceColor)
        {
            if (pieceColor == PieceColor.White)
                return PieceColor.Black;
            else
                return PieceColor.White;
        }
    }
}
