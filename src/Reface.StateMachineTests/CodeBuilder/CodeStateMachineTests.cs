using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.StateMachineTests;

namespace Reface.StateMachine.CodeBuilder.Tests
{
    [TestClass()]
    public class CodeStateMachineTests
    {
        [TestMethod()]
        public void TryPushTest()
        {
            var builder = new CodeStateMachineBuilder<TestStates, TestActions>();
            builder
                .StartWith(TestStates.Default)
                .Move(TestStates.Default, TestActions.Save, TestStates.Draft)
                .Move(TestStates.Draft, TestActions.Check, TestStates.Checked)
                .Move(TestStates.Draft, TestActions.Delete, TestStates.Deleted)
                .Move(TestStates.Checked, TestActions.Check, TestStates.DoubleChcked)
                .Move(TestStates.Checked, TestActions.Recall, TestStates.Recalled)
                .Move(TestStates.DoubleChcked, TestActions.Recall, TestStates.Recalled)
                ;
            var machine = builder.Build();
            Assert.IsFalse(machine.TryPush(TestActions.Check));
            Assert.IsTrue(machine.TryPush(TestActions.Save));
        }
    }
}