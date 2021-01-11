using Moq;
using BooksAndVideos.App.Services;
using BooksAndVideos.App.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace BooksAndVideos.Tests.OrderService
{
    [TestClass]
    public class ProcessTests
    {
        //Mock<ICustomerService> customerService;
        //public ProcessTests()
        //{

        //    customerService.Setup(cs => cs.GetCustomer(1)).Returns(new Customer { Id = 1 });
        //}

        [TestMethod]
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
                            .Callback(()=> { order.Customer.Memberships.Add(MembershipType.BookClubMembership); });

            Assert.IsFalse(order.HasShippingSlip());
        }

        [TestMethod]
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

        [TestMethod]
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

            Assert.IsTrue(order.Customer.Memberships.Count == 1);
            Assert.AreEqual(order.Customer.Memberships.First(), MembershipType.BookClubMembership);
        }
    }
}
