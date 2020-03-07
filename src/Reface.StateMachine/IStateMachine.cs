using Reface.StateMachine.Events;
using System;

namespace Reface.StateMachine
{
    public interface IStateMachine<TState, TAction>
    {
        event EventHandler<StateMachinePushedEventArgs<TState, TAction>> Pushed;
        IStateListener<TState, TAction> GetStateListener(TState state);
        void Push(TAction action);
        bool TryPush(TAction action);
    }
}
