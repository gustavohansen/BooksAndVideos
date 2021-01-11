using BooksAndVideos.App.Services;
using BooksAndVideos.App.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Moq;

namespace BooksAndVideos.Tests.OrderService
{
    [TestFixture()]
    public class ProcessTests
    {
        [Test()]
        public void TestProcessOrderWithOutShippingSlip()
        {
            var order = GetOrder1();

            Mock<ICustomerService> customerService = new Mock<ICustomerService>();

            customerService.Setup(cs => cs.ActivateMembership(order.Customer, MembershipType.BookClubMembership))
                           .Callback(() => { order.Customer.Memberships.Add(MembershipType.BookClubMembership); });

            App.Services.OrderService orderService = new App.Services.OrderService(customerService.Object);

            orderService.Process(order);

            customerService.Verify(cs => cs.ActivateMembership(order.Customer, MembershipType.BookClubMembership), Times.Exactly(1));

            Assert.IsFalse(order.HasShippingSlip());
        }

        [Test()]
        public void TestProcessOrderWithShippingSlip()
        {
            var order = GetOrder2();

            App.Services.OrderService orderService = new App.Services.OrderService(null);

            orderService.Process(order);

            Assert.IsTrue(order.HasShippingSlip());
        }

        [Test()]
        public void TestProcessOrderWithMembership()
        {
            var order = GetOrder1();

            Mock<ICustomerService> customerService = new Mock<ICustomerService>();

            customerService.Setup(cs => cs.ActivateMembership(order.Customer, MembershipType.BookClubMembership))
                           .Callback(() => { order.Customer.Memberships.Add(MembershipType.BookClubMembership); });

       
            App.Services.OrderService orderService = new App.Services.OrderService(customerService.Object);


            orderService.Process(order);

            customerService.Verify(cs => cs.ActivateMembership(order.Customer, MembershipType.BookClubMembership), Times.Exactly(1));

            Assert.True(order.Customer.Memberships.Count == 1);
            Assert.AreEqual(MembershipType.BookClubMembership, order.Customer.Memberships.First());
        }

        [Test()]
        public void TestProcessOrderWithOutMembership()
        {
            var order = GetOrder2();

            App.Services.OrderService orderService = new App.Services.OrderService(null);

            orderService.Process(order);

            Assert.IsEmpty(order.Customer.Memberships);
        }

        private Order GetOrder1()
        {
            return new Order
            {
                Customer = new Customer { Id = 1 },
                Items = new List<OrderLine> { new OrderLine {
                        Id = 1,
                        Count = 1,
                        Product = new Membership
                        {
                            Id = 1,
                            Price = 100,
                            MembershipType = MembershipType.BookClubMembership
                        }
                    }
                }
            };
        }

        private Order GetOrder2()
        {
            return new Order
            {
                Customer = new Customer { Id = 1 },
                Items = new List<OrderLine> { new OrderLine {
                        Id = 2,
                        Count = 1,
                        Product = new PhysicalProduct
                        {
                            Id = 1,
                            Price = 100
                        }
                    }
                }
            };
        }
    }
}
