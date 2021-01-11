using BooksAndVideos.App.Entities;
using System.Linq;

namespace BooksAndVideos.App.Services
{
    public class ShippingService : IShippingService
    {
        public void CreateShippingSlip(Order order)
        {
            if (order.Items.Any(item => item.Product is PhysicalProduct))
            {
                order.ShippingSlip = new ShippingSlip();
            }
        }
    }
}
