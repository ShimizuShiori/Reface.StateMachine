using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Reface.StateMachineTests
{
    [TestClass]
    public class OtherTests
    {
        [TestMethod]
        public void EnumIsStruct()
        {
            Type type = typeof(TestStates);
            Assert.IsTrue(type.IsValueType);
        }

        [TestMethod]
        public void Test2()
        {
            Test<TestStates>(TestStates.Draft);
        }

        private void Test<T>(T? value)
            where T : struct
        {

        }
    }
}
