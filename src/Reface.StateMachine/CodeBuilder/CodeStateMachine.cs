using Reface.StateMachine.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reface.StateMachine.CodeBuilder
{
    public class CodeStateMachine<TState, TAction> : IStateMachine<TState, TAction>
    {
        private readonly IList<StateMoveInfo<TState, TAction>> stateMoveInfos;
        private readonly TState startState;
        private TState currentState;

        public CodeStateMachine(IList<StateMoveInfo<TState, TAction>> stateMoveInfos, TState startState)
        {
            this.stateMoveInfos = stateMoveInfos;
            this.startState = startState;
            this.currentState = startState;
        }

        public event EventHandler<StateMachinePushedEventArgs<TState, TAction>> Pushed;

        public void Push(TAction action)
        {
            var nextMoveInfos = this.stateMoveInfos.Where(x => x.From.Equals(this.currentState) && x.Action.Equals(action));
            if (!nextMoveInfos.Any())
            {
                throw new ApplicationException("没有可用的状态转移");
            }
            if (nextMoveInfos.Count() > 1)
            {
                throw new ApplicationException("有多个可用的状态转移");
            }
            var nextInfo = nextMoveInfos.First();
            this.currentState = nextInfo.To;
            this.Pushed?.Invoke(this, new StateMachinePushedEventArgs<TState, TAction>(nextInfo.From, action, this.currentState));
        }
    }
}
