using BooksAndVideos.App.Entities;

namespace BooksAndVideos.App.Services
{
    public class OrderService : IOrderService
    {
        private ICustomerService customerService;

        public OrderService(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public void Process(Order order)
        {
            bool shouldCreateShippingSlip = false;

            foreach (var item in order.Items)
            {
                if (item.Product is PhysicalProduct)
                {
                    shouldCreateShippingSlip = true;
                }
                else if (item.Product is Membership membership)
                {
                    customerService.ActivateMembership(order.Customer, membership.MembershipType);
                }
            }

            if(shouldCreateShippingSlip)
            {
                CreateShippingSlip(order);
            }
        }

        private void CreateShippingSlip(Order order)
        {
            order.ShippingSlip = new ShippingSlip();
        }
    }
}
