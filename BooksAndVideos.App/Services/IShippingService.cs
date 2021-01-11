using BooksAndVideos.App.Entities;

namespace BooksAndVideos.App.Services
{
    public interface IShippingService
    {
        void CreateShippingSlip(Order order);
    }
}
