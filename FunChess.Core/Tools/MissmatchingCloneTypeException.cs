using System;
using System.Runtime.Serialization;

namespace FunChess.Core.Tools
{
    [Serializable]
    public class MissmatchingCloneTypeException : Exception
    {
        private const string message = "The input and the output should be of the same type. In case this case {right} and not {wrong}";

        public MissmatchingCloneTypeException(Type wrongType, Type rightType)
            : base(message
                    .Replace("{wrong}", wrongType.Name)
                    .Replace("{right}", rightType.Name))
        {
        }

        protected MissmatchingCloneTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
