using Asal.OrderManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asal.OrderManagementSystem.Utilities
{
    public class MyUtilities
    {
        public static Guid ReadGuid(string message)
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();

                if (Guid.TryParse(input, out Guid id))
                    return id;

                Console.WriteLine("Invalid GUID. Please try again.");
            }
        }

        public static decimal ReadPositiveAmount(string message)
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();

                if (decimal.TryParse(input, out decimal amount) && amount > 0)
                    return amount;

                Console.WriteLine("Invalid amount. Please enter a positive number.");
            }
        }
        public static string Read(string message)
        {
            Console.Write(message);
            return Console.ReadLine() ?? string.Empty;
        }


        public static void PrintCustomer(Customer customer)
        {
            Console.WriteLine($"Customer Name  : {customer.Name}");
            Console.WriteLine($"Customer Email : {customer.Email}");
            Console.WriteLine($"Customer ID    : {customer.Id}");
            Console.WriteLine(new string('-', 40));
        }

        public static void PrintCustomers(List<Customer> customers)
        {
            foreach (var customer in customers)
            {
                PrintCustomer(customer);
            }
        }

        public static void PrintOrder(Order order)
        {
            Console.WriteLine($"Order ID: {order.Id}");
            Console.WriteLine($"Customer: {order.CustomerId}");
            Console.WriteLine($"Status: {order.Status}");
            Console.WriteLine($"Created Date: {order.CreatedDate:dd/MM/yyyy}");

            Console.WriteLine("Order Items:");

            foreach (var item in order.OrderItems)
            {
                Console.WriteLine($"Product ID : {item.ProductId}");
                Console.WriteLine($"Quantity   : {item.Quantity}");
                Console.WriteLine($"Unit Price : {item.UnitPrice}");
                Console.WriteLine("----------------------------------------");
            }

            Console.WriteLine("========================================");
        }

        public static void PrintProduct(Product product)
        {
            Console.WriteLine($"Product ID       : {product.Id}");
            Console.WriteLine($"Product Name     : {product.Name}");
            Console.WriteLine($"Product Price    : {product.Price}");
            Console.WriteLine($"Stock Quantity   : {product.StockQuantity}");
            Console.WriteLine("----------------------------------------");
        }

        public static void PrintProducts(List<Product> products)
        {
            foreach (var product in products)
            {
                PrintProduct(product);
            }
        }
    }
}
