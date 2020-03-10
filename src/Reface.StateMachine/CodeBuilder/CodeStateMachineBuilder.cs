using System.Collections.Generic;

namespace Reface.StateMachine.CodeBuilder
{
    public class CodeStateMachineBuilder<TState, TAction> : IStateMachineBuilder<TState, TAction>
    {
        private readonly IList<StateMoveInfo<TState, TAction>> stateMoveInfos
            = new List<StateMoveInfo<TState, TAction>>();
        private TState startState = default(TState);
        private IStateMoveInfoSearcher<TState, TAction> stateMoveInfoSearcher;

        public CodeStateMachineBuilder<TState, TAction> Move(TState from, TAction action, TState to)
        {
            if (this.stateMoveInfoSearcher != null)
                this.stateMoveInfoSearcher = null;
            this.stateMoveInfos.Add(new StateMoveInfo<TState, TAction>(from, action, to));
            return this;
        }

        public CodeStateMachineBuilder<TState, TAction> StartWith(TState state)
        {
            this.startState = state;
            return this;
        }

        public IStateMachine<TState, TAction> Build()
        {
            if (this.stateMoveInfoSearcher == null)
                this.stateMoveInfoSearcher = new DefaultStateMoveInfoSearcher<TState, TAction>(this.stateMoveInfos);
            return new CodeStateMachine<TState, TAction>(this.stateMoveInfoSearcher, this.startState);
        }
    }
}
