using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.StateMachineTests;
using Reface;

namespace Reface.StateMachine.CodeBuilder.Tests
{
    [TestClass()]
    public class CodeStateMachineTests
    {
        [TestMethod()]
        public void TryPush()
        {
            var builder = new CodeStateMachineBuilder<TestStates, TestActions>();
            builder
                .StartWith(TestStates.Default)
                .Move(TestStates.Default, TestActions.Save, TestStates.Draft)
                .Move(TestStates.Draft, TestActions.Check, TestStates.Checked)
                .Move(TestStates.Draft, TestActions.Delete, TestStates.Deleted)
                .Move(TestStates.Checked, TestActions.Check, TestStates.DoubleChecked)
                .Move(TestStates.Checked, TestActions.Recall, TestStates.Recalled)
                .Move(TestStates.DoubleChecked, TestActions.Recall, TestStates.Recalled)
                ;
            var machine = builder.Build();
            Assert.IsFalse(machine.TryPush(TestActions.Check));
            Assert.IsTrue(machine.TryPush(TestActions.Save));
        }

        [TestMethod]
        public void EventOfStoped()
        {
            Event_Finished(new TestActions[] { TestActions.Save, TestActions.Check, TestActions.Check });
            Event_Finished(new TestActions[] { TestActions.Save, TestActions.Delete });
            Event_Finished(new TestActions[] { TestActions.Save, TestActions.Check, TestActions.Recall });
        }

        private void Event_Finished(TestActions[] actions)
        {
            var builder = new CodeStateMachineBuilder<TestStates, TestActions>();
            builder
                .StartWith(TestStates.Default)
                .Move(TestStates.Default, TestActions.Save, TestStates.Draft)
                .Move(TestStates.Draft, TestActions.Check, TestStates.Checked)
                .Move(TestStates.Draft, TestActions.Delete, TestStates.Deleted)
                .Move(TestStates.Checked, TestActions.Check, TestStates.DoubleChecked)
                .Move(TestStates.Checked, TestActions.Recall, TestStates.Recalled)
                .StopWith(TestStates.Deleted, TestStates.DoubleChecked, TestStates.Recalled)
                ;
            var machine = builder.Build();
            bool isFinished = false;
            machine.Stoped += (sender, e) =>
              {
                  isFinished = true;
              };
            foreach (var a in actions)
                machine.Push(a);
            Assert.IsTrue(isFinished, actions.Join(",", x => x.ToString()));
        }
    }
}