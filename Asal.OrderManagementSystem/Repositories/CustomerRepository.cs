using Asal.OrderManagementSystem.Interfaces;
using Asal.OrderManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asal.OrderManagementSystem.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly List<Customer> _customers = new List<Customer>()
        {
             new Customer
            {
                Id = Guid.Parse("3f8a1c2e-7b4d-4f91-a632-9c5e8d1b2047"),
                Name = "abd jarrar",
                Email = "abd@gmail.com"
            },
             new Customer
            {
            Id = Guid.Parse("a72d4e91-6c3f-48b2-9e15-7d8a3f2c6019"),
            Name = "rami ahmad",
            Email = "rami@gmail.com"
            },
             new Customer
            {
            Id = Guid.Parse("b15e9c73-2a64-4d81-8f37-c9e5b2147a06"),
            Name = "yanal salem",
            Email = "yanal@gmail.com"
            }
        };

        public Guid? AddCustomer(string customerName, string customerEmail)
        {
            if (string.IsNullOrWhiteSpace(customerName))
                return null;
            if (string.IsNullOrWhiteSpace(customerEmail))
                return null;

            foreach (var customer in _customers)
            {
                if (string.Equals(customerEmail, customer.Email, StringComparison.OrdinalIgnoreCase))
                    return null;
            }
            var newCustomer = new Customer {
                Id = Guid.NewGuid(),
                Email = customerEmail,
                Name = customerName,
            };
            _customers.Add(newCustomer);
            return newCustomer.Id;

        }

        public List<Customer> GetAllCustomers()
        {
            return _customers.ToList();
        }

        public Customer? GetCustomerById(Guid customerId)
        {
            return _customers.FirstOrDefault(x => x.Id == customerId);
        }
    }
}
