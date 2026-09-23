using Asal.OrderManagementSystem.Interfaces;
using Asal.OrderManagementSystem.Models;
using Asal.OrderManagementSystem.Repositories;
using Asal.OrderManagementSystem.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asal.OrderManagementSystem.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public void AddCustomer()
        {
            string customerName = MyUtilities.Read("Enter customer name: ");
            string customerEmail = MyUtilities.Read("Enter customer email: ");

            Guid? customerId = _customerRepository.AddCustomer(
                customerName,
                customerEmail
            );

            if (customerId is null)
            {
                throw new InvalidOperationException("Failed to add customer.");
            }

            Console.WriteLine($"Customer added successfully. Id: {customerId}");
        }

        public void PrintAllCustomers()
        {
            List<Customer> customers = _customerRepository.GetAllCustomers();

            if (customers.Count == 0)
            {
                Console.WriteLine("No customers found.");
                return;
            }

            MyUtilities.PrintCustomers(customers);
        }
    }
}

