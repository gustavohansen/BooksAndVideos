using BooksAndVideos.App.Entities;

namespace BooksAndVideos.App.Services
{
    public class OrderService : IOrderService
    {
        private readonly ICustomerService customerService;
        private readonly IShippingService shippingService;

        public OrderService(ICustomerService customerService, IShippingService shippingService)
        {
            this.customerService = customerService;
            this.shippingService = shippingService;
        }

        public void Process(Order order)
        {
            foreach (var item in order.Items)
            {
                if (item.Product is Membership membership)
                {
                    customerService.ActivateMembership(order.Customer, membership.MembershipType);
                }
            }

            shippingService.CreateShippingSlip(order);
        }
    }
}
