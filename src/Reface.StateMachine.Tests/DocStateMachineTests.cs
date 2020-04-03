using DocManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Reface.StateMachine.Tests
{
    [TestClass]
    public class DocStateMachineTests
    {
        private readonly DocStateMachineBuilder builder = new DocStateMachineBuilder();

        [TestMethod]
        public void Build()
        {
            var machine = builder.Build();
            DocStates ds = DocStates.Default;
            machine.Pushed += (sender, e) =>
              {
                  ds = e.NewState;
              };
            machine.Push(DocActions.Save);
            Assert.AreEqual(DocStates.Draft, ds);
            machine.Push(DocActions.Delete);
            Assert.AreEqual(DocStates.Deleted, ds);
        }
    }
}
