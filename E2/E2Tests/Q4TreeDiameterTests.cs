using Microsoft.VisualStudio.TestTools.UnitTesting;
using E2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2.Tests
{
    [TestClass()]
    public class Q4TreeDiameterTests//Grade:E2.4:25
    {
        [TestMethod()]
        public void Q4TreeDiameterTest()
        {
            //Assert.Inconclusive();
            Q4TreeDiameter td = new Q4TreeDiameter(10, 0);
            Q4TreeDiameter td2 = new Q4TreeDiameter(15, 2);
        }

        [TestMethod()]
        public void TreeHeightTest()
        {
            //    Q4TreeDiameter td = new Q4TreeDiameter(10, 0);
            //    Assert.AreEqual(4, td.TreeHeight());

            Q4TreeDiameter td2 = new Q4TreeDiameter(15, 2);
            Assert.AreEqual(4, td2.TreeHeight());
        }

        [TestMethod()]
        public void TreeHeightFromNodeTest()
        {
            Q4TreeDiameter td = new Q4TreeDiameter(10, 0);
            //Assert.AreEqual(6, td.TreeHeightFromNode(9));
            //Assert.AreEqual(6, td.TreeHeightFromNode(2));
            //Assert.AreEqual(5, td.TreeHeightFromNode(6));
            Assert.AreEqual(6, td.TreeHeightFromNode(5));

            Q4TreeDiameter td2 = new Q4TreeDiameter(15, 2);
            Assert.AreEqual(7, td2.TreeHeightFromNode(9));
            Assert.AreEqual(5, td2.TreeHeightFromNode(13));
            //Assert.Inconclusive();
        }

        [TestMethod()]
        public void TreeDiameterN2Test()
        {
            //Q4TreeDiameter td = new Q4TreeDiameter(10, 0);
            //Assert.AreEqual(6, td.TreeDiameterN2());

            Q4TreeDiameter td2 = new Q4TreeDiameter(15, 2);
            Assert.AreEqual(7, td2.TreeDiameterN2());
            //Assert.Inconclusive();
        }

        [TestMethod()]
        public void TreeDiameterNTest()
        {
            Assert.Inconclusive();
        }
    }
}