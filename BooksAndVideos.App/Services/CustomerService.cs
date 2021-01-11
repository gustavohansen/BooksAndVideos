using BooksAndVideos.App.Entities;
using System.Linq;

namespace BooksAndVideos.App.Services
{
    public class CustomerService : ICustomerService
    {
        public void ActivateMembership(Customer customer, MembershipType membership)
        {
            if (!customer.Memberships.Any(m => m == membership))
            {
                customer.Memberships.Add(membership);
            }
        }
    }
}
