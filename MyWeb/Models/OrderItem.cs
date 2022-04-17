using System;
using System.Collections.Generic;

namespace MyWeb.Models
{
    public partial class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public Orders Order { get; set; }
        public Products Product { get; set; }
    }
}
