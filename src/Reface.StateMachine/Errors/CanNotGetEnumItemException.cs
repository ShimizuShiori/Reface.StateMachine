using System;

namespace Reface.StateMachine.Errors
{
    public class CanNotGetEnumItemException : Exception
    {
        public const string MESSAGE_NOT_ENUM_TYPE = "not enum type";

        private CanNotGetEnumItemException(string message) : base(message)
        {

        }

        public static CanNotGetEnumItemException CreateNotEnumType()
        {
            return new CanNotGetEnumItemException(CanNotGetEnumItemException.MESSAGE_NOT_ENUM_TYPE);
        }
    }
}
