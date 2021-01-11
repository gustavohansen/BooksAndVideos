using BooksAndVideos.App.Services;
using BooksAndVideos.App.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Moq;

namespace BooksAndVideos.Tests.Services
{
    [TestFixture()]
    public class ShippingServiceTest : BaseTest
    {

        [Test()]
        public void TestCreateShippingSlip()
        {
            var order = GetOrderWithPhysicalProduct();

            new ShippingService().CreateShippingSlip(order);

            Assert.IsTrue(order.HasShippingSlip());
        }

        [Test()]
        public void TestDoNotCreateShippingSlip()
        {
            var order = GetOrderWithMembership();

            new ShippingService().CreateShippingSlip(order);

            Assert.IsFalse(order.HasShippingSlip());
        }
    }
}
