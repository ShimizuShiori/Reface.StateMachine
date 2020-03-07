using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.StateMachine.CodeBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.StateMachineTests
{

    [TestClass()]
    public class ListenerTest
    {
        [TestMethod]
        public void TestGetListener()
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
            int i = 0;
            machine.GetStateListener(TestStates.Draft).Entered
                += (sender, e) =>
                {
                    i++;
                };
            machine.Push(TestActions.Save);
            Assert.AreEqual(1, i);
        }
    }
}
