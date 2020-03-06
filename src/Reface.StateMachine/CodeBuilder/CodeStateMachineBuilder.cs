using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.StateMachine.CodeBuilder
{
    public class CodeStateMachineBuilder<TState, TAction> : IStateMachineBuilder<TState, TAction>
    {
        private readonly IList<StateMoveInfo<TState, TAction>> stateMoveInfos
            = new List<StateMoveInfo<TState, TAction>>();
        private TState startState = default(TState);

        public CodeStateMachineBuilder<TState, TAction> Move(TState from, TAction action, TState to)
        {
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
            return new CodeStateMachine<TState, TAction>(this.stateMoveInfos, this.startState);
        }
    }
}
