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

        public CodeStateMachine(IStateMoveInfoSearcher<TState, TAction> stateMoveInfoSearcher, TState startState)
        {
            this.stateMoveInfoSearcher = stateMoveInfoSearcher;
            this.currentState = startState;
        }

        public event EventHandler<StateMachinePushedEventArgs<TState, TAction>> Pushed;

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
            DebugLogger.Debug($"Find target state :[{nextInfo.To.ToString()}]");
            DebugLogger.Debug("Triggering event : OnLeaving");
            this.GetStateListenerAsDefaultStateListener(this.currentState).OnLeaving(this, new StateLeavingEventArgs<TState, TAction>(action, nextInfo.To));
            this.currentState = nextInfo.To;
            DebugLogger.Debug("Triggering event : OnEntered");
            this.GetStateListenerAsDefaultStateListener(this.currentState).OnEntered(this, new StateEnteredEventArgs<TState, TAction>(action, nextInfo.From));
            DebugLogger.Debug("Triggering event : Pushed");
            this.Pushed?.Invoke(this, new StateMachinePushedEventArgs<TState, TAction>(nextInfo.From, action, this.currentState));

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
