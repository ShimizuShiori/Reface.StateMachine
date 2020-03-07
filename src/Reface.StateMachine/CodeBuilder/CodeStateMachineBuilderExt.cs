namespace Reface.StateMachine.CodeBuilder
{
    public static class CodeStateMachineBuilderExt
    {
        public static FromStep<TState, TAction> From<TState, TAction>(this CodeStateMachineBuilder<TState, TAction> builder, TState state)
        {
            return new FromStep<TState, TAction>(builder, state);
        }
    }
}
