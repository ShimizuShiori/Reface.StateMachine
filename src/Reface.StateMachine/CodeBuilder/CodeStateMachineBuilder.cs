using Reface.StateMachine.Attributes;
using Reface.StateMachine.Errors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reface.StateMachine.CodeBuilder
{
    public class CodeStateMachineBuilder<TState, TAction> : IStateMachineBuilder<TState, TAction>
        where TState : struct
        where TAction : struct
    {
        private readonly IList<StateMoveInfo<TState, TAction>> stateMoveInfos
            = new List<StateMoveInfo<TState, TAction>>();
        private TState? startState;
        private IStateMoveInfoSearcher<TState, TAction> stateMoveInfoSearcher;
        private HashSet<TState> stopStateSet;

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

        public CodeStateMachineBuilder<TState, TAction> StopWith(params TState[] states)
        {
            this.stopStateSet = new HashSet<TState>(states);
            return this;
        }

        public IStateMachine<TState, TAction> Build()
        {
            if (this.stateMoveInfoSearcher == null)
                this.stateMoveInfoSearcher = new DefaultStateMoveInfoSearcher<TState, TAction>(this.stateMoveInfos);

            if (!IsDefaultStateExists())
                this.startState = GetDefaultState();

            if (!IsDefaultStateExists())
                throw new CodeStateMachineBuilderBuildException("没有指定默认状态，无法构建");

            if(!IsStopStateExists())
                this.stopStateSet = new HashSet<TState>(GetStopStates());
            if (!IsStopStateExists())
                this.stopStateSet = new HashSet<TState>();

            return new CodeStateMachine<TState, TAction>(this.stateMoveInfoSearcher, this.startState.Value, this.stopStateSet);
        }

        private TState GetDefaultState()
        {
            var fields = EnumHelper.GetItemsByAttribute<TState, StartStateAttribute>();
            if (fields.Count != 1) return default(TState);
            return (TState)Enum.Parse(typeof(TState), fields[0].Name);
        }

        private IEnumerable<TState> GetStopStates()
        {
            var fields = EnumHelper.GetItemsByAttribute<TState, StopStateAttribute>();
            return fields.Select(x => (TState)Enum.Parse(typeof(TState), x.Name));
        }

        private bool IsDefaultStateExists()
        {
            return this.startState != null && this.startState.HasValue;
        }
        private bool IsStopStateExists()
        {
            return this.stopStateSet != null;
        }
    }
}
