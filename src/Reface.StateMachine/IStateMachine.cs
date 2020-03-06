using Reface.StateMachine.Events;
using System;

namespace Reface.StateMachine
{
    public interface IStateMachine<TState, TAction>
    {
        event EventHandler<StateMachinePushedEventArgs<TState, TAction>> Pushed;

        void Push(TAction action);
    }
}
