using System;
using System.Collections.Generic;
using System.Text;

namespace Asal.OrderManagementSystem.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }
    }
}
