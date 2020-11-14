using FunChess.Core.Enums;

namespace FunChess.XUnitTests.TestTools.Helpers
{
    public class TestHelper
    {
        public TestHelper()
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
