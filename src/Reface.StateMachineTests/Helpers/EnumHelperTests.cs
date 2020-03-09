using Reface.StateMachine.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.StateMachineTests;
using System.Reflection;
using Reface.StateMachine.Attributes;

namespace Reface.StateMachine.Helpers.Tests
{
    [TestClass()]
    public class EnumHelperTests
    {
        [TestMethod()]
        public void GetItemsTest()
        {
            FieldInfo[] fieldInfos = EnumHelper.GetItems<TestStates>();
            Assert.AreEqual(6, fieldInfos.Length);
            foreach (var info in fieldInfos)
                Assert.IsNotNull(info);
        }

        [TestMethod()]
        public void GetItemsByAttributeTest()
        {
            var fieldInfos = EnumHelper.GetItemsByAttribute<TestStates, StartStateAttribute>();
            Assert.AreEqual(1, fieldInfos.Count);
            Assert.AreEqual("Default", fieldInfos[0].Name);
        }
    }
}