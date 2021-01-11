using System.Collections.Generic;

namespace BooksAndVideos.App.Entities
{
    public class Order : Entity
    {
        public Customer Customer { get; set; }

        public IList<OrderLine> Items { get; set; }

        public ShippingSlip ShippingSlip { get; set; }

        public bool HasShippingSlip() { return ShippingSlip != null; }

        public decimal Total
        {
            get
            {
                decimal total = 0;

                foreach (var item in Items)
                {
                    total += item.Product.Price * item.Count;
                }

                return total;
            }
        }
    }
}