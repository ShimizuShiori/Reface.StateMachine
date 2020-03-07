using System;

namespace Reface.StateMachine.Events
{
    public class StateEnteredEventArgs<TState, TAction> : EventArgs
    {
        public TAction Action { get; private set; }
        public TState OldState { get; private set; }

        public StateEnteredEventArgs(TAction action, TState oldState)
        {
            Action = action;
            OldState = oldState;
        }
    }
}
