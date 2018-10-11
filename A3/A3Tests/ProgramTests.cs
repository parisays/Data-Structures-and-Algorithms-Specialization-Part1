using Microsoft.VisualStudio.TestTools.UnitTesting;
using A3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace A3.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A3_TestData")]
        public void Graded_FibonacciTest()
        {
            TestCommon.TestTools.RunLocalTest("A3",Program.ProcessFibonacci, "TD1");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A3_TestData")]
        public void Graded_FibonacciLastDigitTest()
        {
            TestCommon.TestTools.RunLocalTest("A3", Program.ProcessFibonacci_LastDigit, "TD2");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A3_TestData")]
        public void Graded_GCDTest()
        {
            TestCommon.TestTools.RunLocalTest("A3", Program.ProcessGCD, "TD3");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A3_TestData")]
        public void Graded_LCMTest()
        {
            TestCommon.TestTools.RunLocalTest("A3", Program.ProcessLCM, "TD4");
        }

        [TestMethod()]
        [DeploymentItem("TestData", "A3_TestData")]
        public void Graded_FibonacciModTest()
        {
            TestCommon.TestTools.RunLocalTest("A3", Program.ProcessFibonacci_Mod, "TD5");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A3_TestData")]
        public void Graded_FibonacciSumTest()
        {
            TestCommon.TestTools.RunLocalTest("A3", Program.ProcessFibonacci_Sum, "TD6");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A3_TestData")]
        public void Graded_FibonacciPartialSumTest()
        {
            TestCommon.TestTools.RunLocalTest("A3", Program.ProcessFibonacci_Partial_Sum, "TD7");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A3_TestData")]
        public void Graded_FibonacciSumSquaresTest()
        {
            TestCommon.TestTools.RunLocalTest("A3", Program.ProcessFibonacci_Sum_Squares, "TD8");
        }

        [TestMethod()]
        public void GCDTest()
        {
            Assert.AreEqual(10, Program.GCD(10, 10));
        }

        [TestMethod()]
        public void FSumTest()
        {
            Assert.AreEqual(2, Program.Fibonacci_Sum(2));
        }

        [TestMethod()]
        public void FPartialSum()
        {
            Assert.AreEqual(3, Program.Fibonacci_Partial_Sum(150482468097531, 479));
        }

        [TestMethod()]
        public void FTest()
        {
            Assert.AreEqual(233, Program.Fibonacci(13));
        }

        [TestMethod()]
        public void FSquareSumTest()
        {
            Assert.AreEqual(0, Program.Fibonacci_Sum_Squares(5));
        }

        [TestMethod()]
        public void FLastDigit()
        {
            Assert.AreEqual(3, Program.Fibonacci_LastDigit(150482468097533));
        }

        [TestMethod()]
        public void FModTest()
        {
            Assert.AreEqual(161, Program.Fibonacci_Mod(239, 1000));
        }
    }
}