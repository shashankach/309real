using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FarmersMercatoAppTest
{
    [TestClass]
    public class TestBasicCalculations
    {

        [TestMethod]
        public void TestTotalPrice1()
        {
            BasicTests TestTotal = new BasicTests();

            List<Models.Product> products = new List<Models.Product>();

            double output = TestTotal.TotalPrice(products);
            Assert.AreEqual(0, output, 0);
        }

        [TestMethod]
        public void TestTotalPrice2()
        {
            BasicTests TestTotal = new BasicTests();

            List<Models.Product> products = new List<Models.Product>();

            Models.Product product = {
                                        "name": "carrots",
                                        "price": 2.99,
                                        "seller": "growing grounds",
                                        "image": "null"
            };

            products.Add(product);
            products.RemoveAt(0);

            double output = TestTotal.TotalPrice(products);
            Assert.AreEqual(0, output, 0);
        }
    }
}
