using System;
using System.Collections.Generic;

namespace MyWeb.Models
{
    public partial class Products
    {
        public Products()
        {
            OrderItem = new HashSet<OrderItem>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? Size { get; set; }
        public string Varienty { get; set; }
        public double? Price { get; set; }
        public string Status { get; set; }

        public ICollection<OrderItem> OrderItem { get; set; }
    }
}
