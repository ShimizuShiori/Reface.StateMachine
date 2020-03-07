namespace Reface.StateMachine.CodeBuilder
{
    public class FromStep<TState, TAction>
    {
        private readonly CodeStateMachineBuilder<TState, TAction> codeStateMachineBuilder;
        private readonly TState from;

        public FromStep(CodeStateMachineBuilder<TState, TAction> codeStateMachineBuilder, TState from)
        {
            this.codeStateMachineBuilder = codeStateMachineBuilder;
            this.from = from;
        }

        public ToStep<TState, TAction> When(TAction action)
        {
            return new ToStep<TState, TAction>(
                    this.codeStateMachineBuilder,
                    this.from,
                    action,
                    this
                );
        }

        public FromStep<TState, TAction> From(TState state)
        {
            return this.codeStateMachineBuilder.From(state);
        }
    }
}
