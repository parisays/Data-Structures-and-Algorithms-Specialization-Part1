﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using A9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A9.Tests
{
    [TestClass()]//Grade:A9:100
    public class GradedTests
    {
        [TestMethod(), Timeout(3000)]
        [DeploymentItem("TestData", "A9_TestData")]
        public void SolveTest()
        {
            Processor[] problems = new Processor[] {
                new ConvertIntoHeap("TD1"),
                new ParallelProcessing("TD2"),
                new MergingTables("TD3") 
            };

            foreach (var p in problems)
            {
                TestTools.RunLocalTest("A9", p.Process, p.TestDataName);
            }
        }

        [TestMethod() ]
        [DeploymentItem("TestData", "A9_TestData")]
        public void ConvertIntoHeapTest()
        {
            ConvertIntoHeap p = new ConvertIntoHeap("TD1");
            TestTools.RunLocalTest("A9", p.Process, p.TestDataName);
        }

        [TestMethod()]
        [DeploymentItem("TestData", "A9_TestData")]
        public void ParallelProcessingTest()
        {
            ParallelProcessing p = new ParallelProcessing("TD2");
            TestTools.RunLocalTest("A9", p.Process, p.TestDataName);
        }

        [TestMethod()]
        [DeploymentItem("TestData", "A9_TestData")]
        public void MergingTablesTest()
        {
            MergingTables p = new MergingTables("TD3");
            TestTools.RunLocalTest("A9", p.Process, p.TestDataName);
        }
    }
}