using BooksAndVideos.App.Entities;

namespace BooksAndVideos.App.Services
{
    public interface ICustomerService
    {
        void ActivateMembership(Customer customer, MembershipType membership);
    }
}
