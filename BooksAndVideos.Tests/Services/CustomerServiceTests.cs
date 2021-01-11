using BooksAndVideos.App.Services;
using BooksAndVideos.App.Entities;
using NUnit.Framework;

namespace BooksAndVideos.Tests.Services
{
    [TestFixture()]
    public class CustomerServiceTests
    {
        [Test()]
        public void TestActivateMembership()
        {
            var customer = new Customer { Id = 1 };

            Assert.IsEmpty(customer.Memberships);

            new CustomerService().ActivateMembership(customer, MembershipType.Other);

            Assert.IsTrue(customer.Memberships.Count == 1);
            Assert.IsTrue(customer.Memberships.Contains(MembershipType.Other));
        }

        [Test()]
        public void TestActivateMembershipsPreventDuplicates()
        {
            var customer = new Customer { Id = 1 };

            Assert.IsEmpty(customer.Memberships);

            var customerService = new CustomerService();

            customerService.ActivateMembership(customer, MembershipType.Other);
            customerService.ActivateMembership(customer, MembershipType.Other);
            customerService.ActivateMembership(customer, MembershipType.BookClubMembership);

            Assert.IsTrue(customer.Memberships.Count == 2);
            Assert.IsTrue(customer.Memberships.Contains(MembershipType.Other));
            Assert.IsTrue(customer.Memberships.Contains(MembershipType.BookClubMembership));
        }
    }
}
