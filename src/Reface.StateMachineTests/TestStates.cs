using Reface.StateMachine.Attributes;

namespace Reface.StateMachineTests
{
    public enum TestStates
    {
        [StartState]
        Default,
        Draft,
        Checked,
        [StopState]
        DoubleChecked,
        [StopState]
        Deleted,
        [StopState]
        Recalled
    }
}
