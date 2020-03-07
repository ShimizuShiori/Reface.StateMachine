using System;

namespace Reface.StateMachine.Events
{
    public class StateLeavingEventArgs<TState, TAction> : EventArgs
    {
        public TAction Action { get; private set; }
        public TState NewState { get; private set; }

        public StateLeavingEventArgs(TAction action, TState newState)
        {
            Action = action;
            NewState = newState;
        }
    }
}
