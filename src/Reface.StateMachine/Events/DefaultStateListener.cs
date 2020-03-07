using System;

namespace Reface.StateMachine.Events
{
    public class DefaultStateListener<TState, TAction> : IStateListener<TState, TAction>
    {
        public event EventHandler<StateLeavingEventArgs<TState, TAction>> Leaving;
        public event EventHandler<StateEnteredEventArgs<TState, TAction>> Entered;

        public void OnLeaving(IStateMachine<TState, TAction> machine, StateLeavingEventArgs<TState, TAction> e)
        {
            this.Leaving?.Invoke(machine, e);
        }

        public void OnEntered(IStateMachine<TState, TAction> machine, StateEnteredEventArgs<TState, TAction> e)
        {
            this.Entered?.Invoke(machine, e);
        }
    }
}
