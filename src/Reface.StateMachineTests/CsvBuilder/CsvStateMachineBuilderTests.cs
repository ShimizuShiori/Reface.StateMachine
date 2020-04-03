using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.StateMachine.CsvBuilder;

namespace Reface.StateMachineTests.CsvBuilder
{
    [TestClass]
    public class CsvStateMachineBuilderTests
    {
        [TestMethod]
        public void CreateBuilderFromFileDoNotThrowError()
        {
            var builder = CsvStateMachineBuilder<TestStates, TestActions>.FromFile("./Resources/Doc.csv");
        }

        [TestMethod]
        public void BuildAndDoNotThrowError()
        {
            var builder = CsvStateMachineBuilder<TestStates, TestActions>.FromFile("./Resources/Doc.csv");
            var machine = builder.Build();
        }

        [TestMethod]
        public void PushAndCheckStateIsRightByEventOfPushed()
        {
            var builder = CsvStateMachineBuilder<TestStates, TestActions>.FromFile("./Resources/Doc.csv");
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
            Assert.AreEqual(TestStates.DoubleChecked, ts);
        }

        [TestMethod]
        public void BuildFromStream()
        {

        }
    }
}
