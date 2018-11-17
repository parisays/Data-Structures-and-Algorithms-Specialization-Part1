using Microsoft.VisualStudio.TestTools.UnitTesting;
using A7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7.Tests
{
    [TestClass()]
    public class GradedTests
    {
        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A7_TestData")]
        public void SolveTest()
        {
            Processor[] problems = new Processor[] {
                new MaximumGold("TD1"),
                new PartitioningSouvenirs("TD2"),
                new MaximizingArithmeticExpression("TD3")
            };

            foreach (var p in problems)
            {
                TestTools.RunLocalTest("A7", p.Process, p.TestDataName);
            }
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A7_TestData")]
        public void MaximumGoldTest()
        {
            MaximumGold mg = new MaximumGold("TD1");
            TestTools.RunLocalTest("A7", mg.Process, "TD1");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A7_TestData")]
        public void MaximizingArithmeticExpressionTest()
        {
            MaximizingArithmeticExpression @object = new MaximizingArithmeticExpression("TD3");
            TestTools.RunLocalTest("A7", @object.Process, "TD3");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A7_TestData")]
        public void PartitioningSouvenirsTest()
        {
            PartitioningSouvenirs @object = new PartitioningSouvenirs("TD2");
            TestTools.RunLocalTest("A7", @object.Process, "TD2");
        }
    }
}


