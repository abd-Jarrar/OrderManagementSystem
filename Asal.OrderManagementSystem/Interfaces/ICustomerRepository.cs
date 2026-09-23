using Asal.OrderManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asal.OrderManagementSystem.Interfaces
{
    public interface ICustomerRepository
    {
        public Customer? GetCustomerById(Guid customerId);

        public Guid? AddCustomer(string customerName, string customerEmail);

        public List<Customer> GetAllCustomers();
    }
}
