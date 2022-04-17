using System;
using System.Collections.Generic;

namespace MyWeb.Models
{
    public partial class Orders
    {
        public Orders()
        {
            OrderItem = new HashSet<OrderItem>();
        }

        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public double Total { get; set; }
        public int CustomerId { get; set; }
        public int SalePersonId { get; set; }
        public string Status { get; set; }

        public Customers Customer { get; set; }
        public SalePersons SalePerson { get; set; }
        public ICollection<OrderItem> OrderItem { get; set; }
    }
}
