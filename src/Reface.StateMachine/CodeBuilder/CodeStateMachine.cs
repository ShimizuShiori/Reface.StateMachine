using Reface.StateMachine.Events;
using System;
using System.Collections.Generic;

namespace Reface.StateMachine.CodeBuilder
{
    public class CodeStateMachine<TState, TAction> : IStateMachine<TState, TAction>
    {
        private readonly IStateMoveInfoSearcher<TState, TAction> stateMoveInfoSearcher;
        private TState currentState;
        private readonly Dictionary<TState, DefaultStateListener<TState, TAction>> listeners = new Dictionary<TState, DefaultStateListener<TState, TAction>>();
        private readonly HashSet<TState> stopStates;

        public CodeStateMachine(IStateMoveInfoSearcher<TState, TAction> stateMoveInfoSearcher, TState startState, HashSet<TState> stopState)
        {
            this.stateMoveInfoSearcher = stateMoveInfoSearcher;
            this.currentState = startState;
            this.stopStates = stopState;
        }

        public event EventHandler<StateMachinePushedEventArgs<TState, TAction>> Pushed;
        public event EventHandler<StateMachineStopedEventArgs<TState, TAction>> Stoped;

        public IStateListener<TState, TAction> GetStateListener(TState state)
        {
            return this.GetStateListenerAsDefaultStateListener(state);
        }

        private DefaultStateListener<TState, TAction> GetStateListenerAsDefaultStateListener(TState state)
        {
            DefaultStateListener<TState, TAction> result;
            if (!listeners.TryGetValue(state, out result))
            {
                result = new DefaultStateListener<TState, TAction>();
                this.listeners[state] = result;
            }
            return result;
        }

        public void Push(TAction action)
        {
            var nextInfo = this.stateMoveInfoSearcher.Search(this.currentState, action);

            this.GetStateListenerAsDefaultStateListener(this.currentState).OnLeaving(this, new StateLeavingEventArgs<TState, TAction>(action, nextInfo.To));
            this.currentState = nextInfo.To;
            this.GetStateListenerAsDefaultStateListener(this.currentState).OnEntered(this, new StateEnteredEventArgs<TState, TAction>(action, nextInfo.From));

            this.Pushed?.Invoke(this, new StateMachinePushedEventArgs<TState, TAction>(nextInfo.From, action, this.currentState));
            if (this.stopStates.Contains(this.currentState))
                this.Stoped?.Invoke(this, new StateMachineStopedEventArgs<TState, TAction>(nextInfo.From, action, this.currentState));
        }

        public bool TryPush(TAction action)
        {
            try
            {
                this.Push(action);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
