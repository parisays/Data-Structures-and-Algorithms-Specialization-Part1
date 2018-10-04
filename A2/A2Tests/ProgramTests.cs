using Microsoft.VisualStudio.TestTools.UnitTesting;
using A2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace A2.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        [DeploymentItem("TestData", "A2_TestData")]
        public void GradedTest_Correctness()
        {
            TestCommon.TestTools.RunLocalTest("A2", Program.Process);
        }

        [TestMethod() , Timeout(500)]
        [DeploymentItem("TestData", "A2_TestData")]
        public void GradedTest_Performance()
        {
            TestCommon.TestTools.RunLocalTest("A2", Program.Process);
        }

        [TestMethod()]
        public void GradedTest_Sterss()
        {
            Random random = new Random();
            Stopwatch s = new Stopwatch();
            s.Start();
            while (s.Elapsed < TimeSpan.FromSeconds(5))
            {
                int n = random.Next(5, 10);
                List<int> myNumbers = new List<int>(n);
                myNumbers = Enumerable.Range(0, n).Select(x => x = random.Next(1,10)).ToList();

                int result1 = Program.NaiveMaxPairwiseProduct(myNumbers);
                int result2 = Program.FastMaxPairwiseProduct(myNumbers);

                Assert.AreEqual(result1, result2);
            }
            s.Stop();
        }

        [TestMethod()]
        public void NaiveMaxPairwiseProductTest()
        {
            int result = Program.NaiveMaxPairwiseProduct(new List<int> { 1, 10, 100, 200, 56 });

            Assert.AreEqual(20_000, result);
        }

        [TestMethod()]
        public void FastMaxPairwiseProductTest()
        {
            int result = Program.FastMaxPairwiseProduct(new List<int> { 1, 10, 100, 200, 56 });

            Assert.AreEqual(20_000, result);
        }
    }
}