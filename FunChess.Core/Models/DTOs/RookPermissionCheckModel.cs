using FunChess.Core.Enums;

namespace FunChess.Core.Models.DTOs
{
    internal class RookPermissionCheckModel
    {
        public delegate bool loopCondition(int first, int second);

        public bool IsPositiveOrientation { get; internal set; }
        public Direction Direction { get; internal set; }
        public int StartIndex { get; internal set; }
        public int EndIndex { get; internal set; }
        public loopCondition LoopCondition { get; internal set; }
    }
}