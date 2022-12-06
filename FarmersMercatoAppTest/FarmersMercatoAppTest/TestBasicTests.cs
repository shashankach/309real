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
            
            Models.Product product = {
                                        "name": "carrots",
                                        "price": 2.99,
                                        "seller": "growing grounds",
                                        "image": "null"
             };

            products.Add(product);

            double output = TestTotal.TotalPrice(products);
            Assert.AreEqual(2.99, output, 0);
        }

        [TestMethod]
        public void TestTotalPrice2()
        {
            BasicTests TestTotal = new BasicTests();

            List<Models.Product> products = new List<Models.Product>();

            Models.Product product1 = {
                                        "name": "carrots",
                                        "price": 2.99,
                                        "seller": "growing grounds",
                                        "image": "null"
            };

            Models.Product product2 = {
                                        "name": "apples",
                                        "price": 3.00,
                                        "seller": "growing grounds",
                                        "image": "null"
            };

            products.Add(product1);
            products.Add(product2);

            double output = TestTotal.TotalPrice(products);
            Assert.AreEqual(5.99, output, 0);
        }
    }
}
