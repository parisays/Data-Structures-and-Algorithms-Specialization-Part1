using Microsoft.VisualStudio.TestTools.UnitTesting;
using A8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8.Tests
{
    [TestClass()]//Grade:A8:100
    public class GradedTests
    {
        [TestMethod(), Timeout(2000)]
        [DeploymentItem("TestData", "A8_TestData")]
        public void SolveTest()
        {
            Processor[] problems = new Processor[] {
                new CheckBrackets("TD1"),
                new TreeHeight("TD2"),
                new PacketProcessing("TD3")
            };
            
            foreach (var p in problems)
            {
                TestTools.RunLocalTest("A8", p.Process, p.TestDataName);
            }
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A8_TestData")]
        public void CheckBracketsTest()
        {
            CheckBrackets checkBrackets = new CheckBrackets("TD1");
            TestTools.RunLocalTest("A8", checkBrackets.Process, "TD1");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A8_TestData")]
        public void TreeHeightTest()
        {
            TreeHeight treeHeight = new TreeHeight("TD2");
            TestTools.RunLocalTest("A8", treeHeight.Process, "TD2");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A8_TestData")]
        public void PacketProcessingTest()
        {
            PacketProcessing packetProcessing = new PacketProcessing("TD3");
            TestTools.RunLocalTest("A8", packetProcessing.Process, "TD3");
        }
    }
    
}