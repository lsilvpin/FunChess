using FunChess.Core.Enums;
using System;
using System.Runtime.Serialization;

namespace FunChess.Core.Exceptions
{
    [Serializable]
    public class PieceOutOfBoardException : Exception
    {
        private const string message = "The {color} {type} i working with should not be out of board.";

        public PieceOutOfBoardException(Type type, PieceColor color)
            : base(message.Replace("{type}", type.Name).Replace("{color}", color.ToString()))
        {
        }

        protected PieceOutOfBoardException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
