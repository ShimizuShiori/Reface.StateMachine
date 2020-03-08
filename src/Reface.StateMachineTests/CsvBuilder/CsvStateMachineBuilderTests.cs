//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Reface.StateMachine.CsvBuilder;
//using Reface.StateMachineTests;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Reface.StateMachine.CsvBuilder.Tests
//{
//    [TestClass()]
//    public class CsvStateMachineBuilderTests
//    {
//        [TestMethod()]
//        public void TestCsvBuilder()
//        {
//            var machine = CsvStateMachineBuilder<TestStates, TestActions>
//                            .FromFile("D:\\1.csv").Build();
//            TestStates state = TestStates.Default;
//            machine.Pushed += (sender, e) =>
//            {
//                state = e.NewState;
//            };

//            Assert.IsFalse(machine.TryPush(TestActions.Check));
//            Assert.IsFalse(machine.TryPush(TestActions.Delete));
//            Assert.IsFalse(machine.TryPush(TestActions.Recall));
//            machine.Push(TestActions.Save);
//            Assert.AreEqual(TestStates.Draft, state);

//            Assert.IsFalse(machine.TryPush(TestActions.Save));
//            Assert.IsFalse(machine.TryPush(TestActions.Recall));
//            machine.Push(TestActions.Check);
//            Assert.AreEqual(TestStates.Checked, state);

//            Assert.IsFalse(machine.TryPush(TestActions.Save));
//            Assert.IsFalse(machine.TryPush(TestActions.Delete));
//            machine.Push(TestActions.Check);
//            Assert.AreEqual(TestStates.DoubleChecked, state);

//            machine.Push(TestActions.Recall);
//            Assert.AreEqual(TestStates.Recalled, state);
//        }
//    }
//}