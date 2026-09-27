using System;
using System.Collections.Generic;
using System.Text;

namespace Asal.OrderManagementSystem.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new ();
    }
}
