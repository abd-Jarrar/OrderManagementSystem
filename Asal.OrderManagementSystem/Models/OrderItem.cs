using System;
using System.Collections.Generic;
using System.Text;

namespace Asal.OrderManagementSystem.Models
{
    public class OrderItem
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
