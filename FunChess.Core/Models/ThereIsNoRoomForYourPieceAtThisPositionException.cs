using System;
using System.Runtime.Serialization;

namespace FunChess.Core.Models
{
    [Serializable]
    public class ThereIsNoRoomForYourPieceAtThisPositionException : Exception
    {
        private const string message = "You cannot put your {pieceAtHand} at position {target} because already exists a {pieceAtTarget} in this square.";

        public ThereIsNoRoomForYourPieceAtThisPositionException(Piece pieceAtHand, Piece pieceAtTarget, Position target)
            : base(message.Replace("{pieceAtHand}", pieceAtHand.GetType().Name)
                    .Replace("{target}", target.ToString())
                    .Replace("{pieceAtTarget}", pieceAtTarget.GetType().Name))
        {
        }

        protected ThereIsNoRoomForYourPieceAtThisPositionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
