namespace Reface.StateMachine.CodeBuilder
{
    public static class CodeStateMachineBuilderExt
    {
        public static FromStep<TState, TAction> From<TState, TAction>(this CodeStateMachineBuilder<TState, TAction> builder, TState state)
            where TState : struct
            where TAction : struct
        {
            return new FromStep<TState, TAction>(builder, state);
        }
    }
}
