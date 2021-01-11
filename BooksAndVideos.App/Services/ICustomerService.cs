using BooksAndVideos.App.Entities;

namespace BooksAndVideos.App.Services
{
    public interface ICustomerService
    {
        Customer GetCustomer(int id);

        void ActivateMembership(Customer customer, MembershipType membership);
    }
}
