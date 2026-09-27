using Asal.OrderManagementSystem.Interfaces;
using Asal.OrderManagementSystem.Models;
using Asal.OrderManagementSystem.Repositories;
using Asal.OrderManagementSystem.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asal.OrderManagementSystem.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;

        public OrderService(IOrderRepository orderRepository,IProductRepository productRepository,ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
        }
        public void FilterOrdersByStats()
        {
            OrderStatus status = MyUtilities.ReadOrderStatus();

            List<Order> orders = _orderRepository.GetOrdersByStatus(status);

            if (orders.Count == 0)
            {
                Console.WriteLine($"No orders found with status: {status}");
                return;
            }

            MyUtilities.PrintOrders(orders);
        }

        public void CalculateOrderTotal()
        {
            Guid orderId = MyUtilities.ReadGuid("Enter Order ID: ");

            decimal? total = _orderRepository.CalculateOrderTotalPrice(orderId);

            if (total is null)
            {
                Console.WriteLine("Order not found.");
                return;
            }

            Console.WriteLine($"Order Total: {total.Value:C}");
        }

        public void CancelOrder()
        {
            Guid orderId = MyUtilities.ReadGuid("Enter Order ID: ");

            var order = _orderRepository.GetOrderById(orderId);

            if (order is null)
            {
                Console.WriteLine("Order not found.");
                return;
            }

            if (order.Status == OrderStatus.Cancelled)
            {
                Console.WriteLine("Order is already cancelled.");
                return;
            }

            if (order.Status == OrderStatus.Completed)
            {
                Console.WriteLine("Completed orders cannot be cancelled.");
                return;
            }

            _orderRepository.CancelOrder(orderId);

            Console.WriteLine("Order cancelled successfully.");
        }

        public void DisplayOrder()
        {
            Guid orderId = MyUtilities.ReadGuid("Enter Order ID: ");

            Order? order = _orderRepository.GetOrderById(orderId);

            if (order is null)
            {
                Console.WriteLine("Order not found.");
                return;
            }

            MyUtilities.PrintOrder(order);
        }

        public void DisplayAllOrders()
        {
            List<Order> orders = _orderRepository.GetAllOrders();

            if (orders.Count == 0)
            {
                Console.WriteLine("No orders found.");
                return;
            }

            MyUtilities.PrintOrders(orders);
        }

        public void CreateOrder()
        {
            Guid customerId = MyUtilities.ReadGuid("Enter Customer ID: ");

            var customer = _customerRepository.GetCustomerById(customerId);

            if (customer is null)
            {
                Console.WriteLine("Customer not found.");
                return;
            }

            int numberOfProducts =
                MyUtilities.ReadNonNegativeInt("Enter number of products: ");

            if (numberOfProducts == 0)
            {
                Console.WriteLine("An order must contain at least one product.");
                return;
            }

            var items = new List<OrderItem>();

            for (int i = 0; i < numberOfProducts; i++)
            {
                Console.WriteLine($"\nProduct {i + 1}");

                Guid productId = MyUtilities.ReadGuid("Enter Product ID: ");

                var product = _productRepository.GetProductById(productId);

                if (product is null)
                {
                    Console.WriteLine("Product not found.");
                    return;
                }

                if (product.Price < 0)
                {
                    Console.WriteLine("Product price cannot be negative.");
                    return;
                }

                int quantity = MyUtilities.ReadPositiveInt("Enter Quantity: ");

                if (quantity > product.StockQuantity)
                {
                    Console.WriteLine(
                        $"Insufficient stock. Available quantity: {product.StockQuantity}");
                    return;
                }

                items.Add(new OrderItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    UnitPrice = product.Price
                });
            }

            Guid? orderId = _orderRepository.CreateOrder(customerId, items);

            if (orderId is null)
            {
                Console.WriteLine("Failed to create order.");
                return;
            }

            Console.WriteLine($"Order created successfully.");
            Console.WriteLine($"Order ID: {orderId}");
        }

        public void AddProductToOrder()
        {
            Guid orderId = MyUtilities.ReadGuid("Enter Order ID: ");
            Guid productId = MyUtilities.ReadGuid("Enter Product ID: ");
            int quantity = MyUtilities.ReadPositiveInt("Enter Quantity: ");

            bool success = _orderRepository.AddProductToOrder(
                orderId,
                productId,
                quantity);

            if (!success)
            {
                Console.WriteLine("Failed to add product to order.");
                return;
            }

            Console.WriteLine("Product added to order successfully.");
        }

    }
}
