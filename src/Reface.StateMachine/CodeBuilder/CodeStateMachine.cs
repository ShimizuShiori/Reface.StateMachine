using Reface.StateMachine.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reface.StateMachine.CodeBuilder
{
    public class CodeStateMachine<TState, TAction> : IStateMachine<TState, TAction>
    {
        private readonly IList<StateMoveInfo<TState, TAction>> stateMoveInfos;
        private TState currentState;
        private readonly Dictionary<TState, DefaultStateListener<TState, TAction>> listeners = new Dictionary<TState, DefaultStateListener<TState, TAction>>();

        public CodeStateMachine(IList<StateMoveInfo<TState, TAction>> stateMoveInfos, TState startState)
        {
            this.stateMoveInfos = stateMoveInfos;
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
            Console.WriteLine($"Push : [{this.currentState.ToString()}]--[{action.ToString()}]-->");
            var nextMoveInfos = this.stateMoveInfos.Where(x => x.From.Equals(this.currentState) && x.Action.Equals(action));
            if (!nextMoveInfos.Any())
            {
                Console.WriteLine("不存在的转移");
                throw new ApplicationException("不存在的转移");
            }
            if (nextMoveInfos.Count() > 1)
            {
                Console.WriteLine("有多个可用的状态转移");
                throw new ApplicationException("有多个可用的状态转移");
            }
            var nextInfo = nextMoveInfos.First();
            Console.WriteLine($"Find target state :[{nextInfo.To.ToString()}]");
            Console.WriteLine("Triggering event : OnLeaving");
            this.GetStateListenerAsDefaultStateListener(this.currentState).OnLeaving(this, new StateLeavingEventArgs<TState, TAction>(action, nextInfo.To));
            this.currentState = nextInfo.To;
            Console.WriteLine("Triggering event : OnEntered");
            this.GetStateListenerAsDefaultStateListener(this.currentState).OnEntered(this, new StateEnteredEventArgs<TState, TAction>(action, nextInfo.From));
            Console.WriteLine("Triggering event : Pushed");
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
