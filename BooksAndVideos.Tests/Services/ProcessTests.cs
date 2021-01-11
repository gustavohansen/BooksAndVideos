using BooksAndVideos.App.Services;
using BooksAndVideos.App.Entities;
using NUnit.Framework;
using System.Linq;
using Moq;

namespace BooksAndVideos.Tests.Services
{
    [TestFixture()]
    public class ProcessTests : BaseTest
    {
        Mock<IShippingService> shippingService;
        Mock<ICustomerService> customerService;

        [SetUp()]
        public void SetUp()
        {
            shippingService = new Mock<IShippingService>();
            customerService = new Mock<ICustomerService>();
        }

        [Test()]
        public void TestProcessOrderWithOutShippingSlip()
        {
            var order = GetOrderWithMembership();

            customerService.Setup(cs => cs.ActivateMembership(order.Customer, MembershipType.BookClubMembership))
                           .Callback(() => { order.Customer.Memberships.Add(MembershipType.BookClubMembership); });

            var orderService = new OrderService(customerService.Object, shippingService.Object);

            orderService.Process(order);

            customerService.Verify(cs => cs.ActivateMembership(order.Customer, MembershipType.BookClubMembership), Times.Exactly(1));

            Assert.IsFalse(order.HasShippingSlip());
        }

        [Test()]
        public void TestProcessOrderWithShippingSlip()
        {
            var order = GetOrderWithPhysicalProduct();

            var orderService = new OrderService(customerService.Object, shippingService.Object);

            shippingService.Setup(s => s.CreateShippingSlip(order))
                          .Callback(() => { order.ShippingSlip = new ShippingSlip(); });

            orderService.Process(order);

            shippingService.Verify(s => s.CreateShippingSlip(order), Times.Exactly(1));

            Assert.IsTrue(order.HasShippingSlip());
        }

        [Test()]
        public void TestProcessOrderWithMembership()
        {
            var order = GetOrderWithMembership();

            Mock<ICustomerService> customerService = new Mock<ICustomerService>();

            customerService.Setup(cs => cs.ActivateMembership(order.Customer, MembershipType.BookClubMembership))
                           .Callback(() => { order.Customer.Memberships.Add(MembershipType.BookClubMembership); });

       
            var orderService = new OrderService(customerService.Object, shippingService.Object);

            orderService.Process(order);

            customerService.Verify(cs => cs.ActivateMembership(order.Customer, MembershipType.BookClubMembership), Times.Exactly(1));

            Assert.True(order.Customer.Memberships.Count == 1);
            Assert.AreEqual(MembershipType.BookClubMembership, order.Customer.Memberships.First());
        }

        [Test()]
        public void TestProcessOrderWithOutMembership()
        {
            var order = GetOrderWithPhysicalProduct();

            var orderService = new OrderService(customerService.Object, shippingService.Object);

            orderService.Process(order);

            Assert.IsEmpty(order.Customer.Memberships);
        }
    }
}
