using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.StateMachineTests;

namespace Reface.StateMachine.CodeBuilder.Tests
{
    [TestClass()]
    public class CodeStateMachineBuilderTests
    {
        [TestMethod()]
        public void BuildTest()
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
            TestStates ts = TestStates.Default;
            machine.Pushed += (sender, e) =>
            {
                ts = e.NewState;
            };
            machine.Push(TestActions.Save);
            Assert.AreEqual(TestStates.Draft, ts);
            machine.Push(TestActions.Check);
            Assert.AreEqual(TestStates.Checked, ts);
            machine.Push(TestActions.Check);
            Assert.AreEqual(TestStates.DoubleChcked, ts);
        }

        [TestMethod]
        public void BuildByChain()
        {
            var builder = new CodeStateMachineBuilder<TestStates, TestActions>();
            builder.StartWith(TestStates.Default)
                .From(TestStates.Default)
                    .When(TestActions.Save).To(TestStates.Draft)
                .From(TestStates.Draft)
                    .When(TestActions.Check).To(TestStates.Checked)
                    .When(TestActions.Delete).To(TestStates.Deleted)
                .From(TestStates.Checked)
                    .When(TestActions.Check).To(TestStates.DoubleChcked)
                    .When(TestActions.Recall).To(TestStates.Recalled);
            var machine = builder.Build();
            TestStates ts = TestStates.Default;
            machine.Pushed += (sender, e) =>
              {
                  ts = e.NewState;
              };
            machine.Push(TestActions.Save);
            Assert.AreEqual(TestStates.Draft, ts);
            machine.Push(TestActions.Check);
            Assert.AreEqual(TestStates.Checked, ts);
            machine.Push(TestActions.Check);
            Assert.AreEqual(TestStates.DoubleChcked, ts);
        }
    }
}