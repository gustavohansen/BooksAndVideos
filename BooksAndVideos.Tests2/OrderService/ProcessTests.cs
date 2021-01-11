using Moq;
using BooksAndVideos.App.Services;
using BooksAndVideos.App.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;


namespace BooksAndVideos.Tests.OrderService
{
    [TestFixture()]
    public class ProcessTests
    {
        [Test()]
        public void TestProcessOrderWithOutShippingSlip()
        {
            Order order = new Order
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

            Mock<ICustomerService> customerService = new Mock<ICustomerService>();

            customerService.Setup(cs => cs.ActivateMembership(order.Customer, MembershipType.BookClubMembership))
                            .Callback(() => { order.Customer.Memberships.Add(MembershipType.BookClubMembership); });

            Assert.False(order.HasShippingSlip());
        }

        [Test()]
        public void TestProcessOrderWithShippingSlip()
        {
            //Order order = new Order()
            //{
            //    Customer = new Customer()
            //    {
            //        Id = 1
            //    }
            //}
        }

        [Test()]
        public void TestProcessOrderWithMembership()
        {
            Order order = new Order
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

            Mock<ICustomerService> customerService = new Mock<ICustomerService>();

            customerService.Setup(cs => cs.ActivateMembership(order.Customer, MembershipType.BookClubMembership))
                            .Callback(() => { order.Customer.Memberships.Add(MembershipType.BookClubMembership); });


            BooksAndVideos.App.Services.OrderService orderService = new App.Services.OrderService(null);
            //new OrderService(customerService).Pr

            customerService.VerifyNoOtherCalls();

            Assert.True(order.Customer.Memberships.Count == 1);
            Assert.Equal(MembershipType.BookClubMembership, order.Customer.Memberships.First());
        }
    }
}
