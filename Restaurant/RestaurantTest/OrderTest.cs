using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RestaurantTest
{
    using System.Linq;

    using Restaurant;

    [TestClass]
    public class OrderTest
    {
        private const string Json = @"{
""waiter"": ""Georgie"",
""table"": 2,
""cookTime"": 5,
""tax"": 2.00,
""subTotal"": 10.00,
""total"": 12.00,
""lines"": [
	{ 
		""name"": ""razor blades special"",
		""price"": 2.99,
		""ingredients"": ""wazor blades, hamburger, ice cream, pickles""
	},
	{ 
		""name"": ""icecream"",
		""price"": 2.99,
		""ingredients"": ""icecream""
	}
	],
""card"": ""123456""
}";
        [TestMethod]
        public void WaiterShouldBeGeorgie()
        {
            Order order = new Order(Json);
            Assert.AreEqual("Georgie", order.Waiter);
        }

        [TestMethod]
        public void ThereShouldBeThreeLines()
        {
            Order order = new Order(Json);
            Assert.AreEqual(2, order.Lines.Count());
            Line lastLine = order.Lines.Last();
            Assert.AreEqual("icecream", lastLine.Name);
        }

    }
}
