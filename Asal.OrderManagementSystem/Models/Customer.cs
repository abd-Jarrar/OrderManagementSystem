using System;
using System.Collections.Generic;
using System.Text;

namespace Asal.OrderManagementSystem.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;

    }
}
