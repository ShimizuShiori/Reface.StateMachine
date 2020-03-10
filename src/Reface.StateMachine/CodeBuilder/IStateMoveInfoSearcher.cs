namespace Reface.StateMachine.CodeBuilder
{
    public interface IStateMoveInfoSearcher<TState,TAction>
    {
        StateMoveInfo<TState, TAction> Search(TState from, TAction when);
    }
}
