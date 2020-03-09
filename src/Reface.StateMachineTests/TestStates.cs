using Reface.StateMachine.Attributes;

namespace Reface.StateMachineTests
{
    public enum TestStates
    {
        [StartState]
        Default,
        Draft,
        Checked,
        DoubleChecked,
        Deleted,
        Recalled
    }
}
