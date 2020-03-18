using System;

namespace Reface.StateMachine.Events
{
    public class StateMachineStopedEventArgs<TState, TAction> : EventArgs
    {
        public TState OldState { get; private set; }
        public TAction Action { get; private set; }
        public TState NewState { get; private set; }

        public StateMachineStopedEventArgs(TState oldState, TAction action, TState newState)
        {
            OldState = oldState;
            Action = action;
            NewState = newState;
        }
    }
}
