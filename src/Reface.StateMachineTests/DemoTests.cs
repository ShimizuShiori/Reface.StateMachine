using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.StateMachineTests
{
    [TestClass]
    public class DemoTests
    {
        [TestMethod]
        public void SplitToLines()
        {
            String content = File.ReadAllText("D:\\NPA.csv");
            String[] lines = content.Split(new char[] { '\n' });
            int i = lines.Length;
        }
    }
}
