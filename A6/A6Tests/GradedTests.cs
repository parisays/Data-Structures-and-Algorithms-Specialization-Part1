using Microsoft.VisualStudio.TestTools.UnitTesting;
using A6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6.Tests
{
    [TestClass()]
    public class GradedTests
    {
        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData","A6_TestData")]
        public void SolveTest()
        {
            Processor[] problems = new Processor[] {
                new MoneyChange("TD1"),
                new PrimitiveCalculator("TD2"),
                new EditDistance("TD3"),
                new LCSOfTwo("TD4"),
                new LCSOfThree("TD5")
            };

            foreach(var p in problems)
            {
                TestTools.RunLocalTest("A6", p.Process, p.TestDataName);
            }
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A6_TestData")]
        public void MoneyChangeTest()
        {
            MoneyChange mc = new MoneyChange("TD1");
            TestTools.RunLocalTest("A6", mc.Process, "TD1");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A6_TestData")]
        public void PrimitiveCalculatorTest()
        {
            PrimitiveCalculator pc = new PrimitiveCalculator("TD2");
            TestTools.RunLocalTest("A6", pc.Process, "TD2");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A6_TestData")]
        public void EditDistanceTest()
        {
            EditDistance ed = new EditDistance("TD3");
            TestTools.RunLocalTest("A6", ed.Process, "TD3");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A6_TestData")]
        public void LCSOfTwoTest()
        {
            LCSOfTwo s2 = new LCSOfTwo("TD4");
            TestTools.RunLocalTest("A6", s2.Process, "TD4");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A6_TestData")]
        public void LCSOfThreeTest()
        {
            LCSOfThree s3 = new LCSOfThree("TD5");
            TestTools.RunLocalTest("A6", s3.Process, "TD5");
        }
    }
}