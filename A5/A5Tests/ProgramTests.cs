using Microsoft.VisualStudio.TestTools.UnitTesting;
using A5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5.Tests
{
    [TestClass()]//Grade:A5:100
    public class ProgramTests
    {
        [TestMethod(),Timeout(1000)]
        [DeploymentItem(@"TestData", "A5_TestData")]
        public void Graded_BinarySearch1Test()
        {
            TestTools.RunLocalTest("A5", Program.ProcessBinarySearch1, "TD1");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem(@"TestData", "A5_TestData")]
        public void Graded_MajorityElement2Test()
        {
            TestTools.RunLocalTest("A5", Program.ProcessMajorityElement2, "TD2");
        }


        [TestMethod(), Timeout(1000)]
        [DeploymentItem(@"TestData", "A5_TestData")]
        public void Graded_ImprovingQuickSort3Test()
        {
            TestTools.RunLocalTest("A5", Program.ProcessImprovingQuickSort3, "TD3");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem(@"TestData", "A5_TestData")]
        public void Graded_NumberofInversions4Test()
        {
            TestTools.RunLocalTest("A5", Program.ProcessNumberofInversions4, "TD4");
        }

        [TestMethod()]
        [DeploymentItem(@"TestData", "A5_TestData")]
        public void Graded_OrganizingLottery5Test()
        {
            TestTools.RunLocalTest("A5", Program.ProcessOrganizingLottery5, "TD5");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem(@"TestData", "A5_TestData")]
        public void Graded_ClosestPoints6()
        {
            TestTools.RunLocalTest("A5", Program.ProcessClosestPoints6, "TD6");
        }

        [TestMethod()]
        public void FindMajorityElement()
        {
            Assert.AreEqual(1, Program.MajorityElement2(4, new long[] { 2,3,9,2,2}));
        }

        [TestMethod()]
        public void OrganizingLottery()
        {
            long[] expected = new long[] { 1, 1, 1 };
            long[] result = Program.OrganizingLottery5(new long[] { 1,1,7 }, new long[] { 1 }, new long[] { 7 });
            CollectionAssert.AreEqual(expected, result);
        }

    }
}