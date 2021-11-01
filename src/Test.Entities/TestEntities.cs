using Microsoft.VisualStudio.TestTools.UnitTesting;
using Entities;
using System;
namespace Test.Entities
{
    [TestClass]
    public class TestEntities
    {
        [TestMethod]
        public void TestMethod1()
        {
            Tickets ticket = new Tickets
            {
                TicketID = 1,
                Name = "Movie1",
                ValidTo = Convert.ToDateTime("20-10-2021"),
                Price = 255
            };

            Assert.IsInstanceOfType(ticket, typeof(Tickets));
        }
    }
}