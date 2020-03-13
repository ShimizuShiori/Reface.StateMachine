using System;

namespace Reface.StateMachine.Errors
{
    public class SearchMoveInfoException : Exception
    {
        public const string MESSAGE_NO_MOVE_INFO = "no move info";
        public const string MESSAGE_MOVE_INFO_MORE_THAN_ONE = "move info more than one";

        private SearchMoveInfoException(string message) : base(message)
        {

        }

        public static SearchMoveInfoException CreateByNoMoveInfo()
        {
            return new SearchMoveInfoException(MESSAGE_NO_MOVE_INFO);
        }

        public static SearchMoveInfoException CreateByMoveInfoMoreThanOne()
        {
            return new SearchMoveInfoException(MESSAGE_MOVE_INFO_MORE_THAN_ONE);
        }
    }
}
