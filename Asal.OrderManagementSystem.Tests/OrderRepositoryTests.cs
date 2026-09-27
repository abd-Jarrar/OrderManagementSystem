using Asal.OrderManagementSystem.Models;
using Asal.OrderManagementSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asal.OrderManagementSystem.Tests
{
    public class OrderRepositoryTests
    {
        [Fact]
        public void GetOrdersByStatus_Pending_ReturnsPendingOrders()
        {
            // Arrange
            var productRepository = new ProductRepository();
            var customerRepository = new CustomerRepository();

            var orderRepository = new OrderRepository(
                productRepository,
                customerRepository);

            // Act
            var orders = orderRepository.GetOrdersByStatus(OrderStatus.Pending);

            // Assert
            Assert.Single(orders);
            Assert.Equal(OrderStatus.Pending, orders[0].Status);
        }

        [Fact]
        public void GetOrdersByStatus_Completed_ReturnsCompletedOrders()
        {
            // Arrange
            var productRepository = new ProductRepository();
            var customerRepository = new CustomerRepository();

            var orderRepository = new OrderRepository(
                productRepository,
                customerRepository);

            // Act
            var orders = orderRepository.GetOrdersByStatus(OrderStatus.Completed);

            // Assert
            Assert.Single(orders);
            Assert.Equal(OrderStatus.Completed, orders[0].Status);
        }


        [Fact]
        public void GetOrdersByStatus_Shipped_ReturnsEmptyList()
        {
            // Arrange
            var productRepository = new ProductRepository();
            var customerRepository = new CustomerRepository();

            var orderRepository = new OrderRepository(
                productRepository,
                customerRepository);

            // Act
            var orders = orderRepository.GetOrdersByStatus(OrderStatus.Shipped);

            // Assert
            Assert.Empty(orders);
        }

        [Fact]
        public void CreateOrder_ValidData_ReturnsOrderId()
        {
            // Arrange
            var productRepository = new ProductRepository();
            var customerRepository = new CustomerRepository();

            var orderRepository = new OrderRepository(
                productRepository,
                customerRepository);

            var customerId =
                Guid.Parse("3f8a1c2e-7b4d-4f91-a632-9c5e8d1b2047");

            var productId =
                Guid.Parse("11111111-1111-1111-1111-111111111111");

            var items = new List<OrderItem>
    {
        new OrderItem
        {
            ProductId = productId,
            Quantity = 2,
            UnitPrice = 1200m
        }
    };

            // Act
            Guid? orderId = orderRepository.CreateOrder(
                customerId,
                items);

            // Assert
            Assert.NotNull(orderId);

            var order = orderRepository.GetOrderById(orderId.Value);

            Assert.NotNull(order);
            Assert.Equal(customerId, order.CustomerId);
            Assert.Equal(OrderStatus.Pending, order.Status);
            Assert.Single(order.OrderItems);
        }

        [Fact]
        public void CreateOrder_InvalidCustomer_ReturnsNull()
        {
            // Arrange
            var productRepository = new ProductRepository();
            var customerRepository = new CustomerRepository();

            var orderRepository = new OrderRepository(
                productRepository,
                customerRepository);

            var invalidCustomerId = Guid.NewGuid();

            var items = new List<OrderItem>
    {
        new OrderItem
        {
            ProductId =
                Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Quantity = 1,
            UnitPrice = 1200m
        }
    };

            // Act
            Guid? orderId = orderRepository.CreateOrder(
                invalidCustomerId,
                items);

            // Assert
            Assert.Null(orderId);
        }

        [Fact]
        public void CalculateOrderTotalPrice_ExistingOrder_ReturnsCorrectTotal()
        {
            // Arrange
            var productRepository = new ProductRepository();
            var customerRepository = new CustomerRepository();

            var orderRepository = new OrderRepository(
                productRepository,
                customerRepository);

            var orderId =
                Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            // Act
            decimal? total = orderRepository.CalculateOrderTotalPrice(orderId);

            // Assert
            Assert.NotNull(total);
            Assert.Equal(1350m, total.Value);
        }


        [Fact]
        public void CalculateOrderTotalPrice_InvalidOrderId_ReturnsNull()
        {
            // Arrange
            var productRepository = new ProductRepository();
            var customerRepository = new CustomerRepository();

            var orderRepository = new OrderRepository(
                productRepository,
                customerRepository);

            var invalidOrderId = Guid.NewGuid();

            // Act
            decimal? total =
                orderRepository.CalculateOrderTotalPrice(invalidOrderId);

            // Assert
            Assert.Null(total);
        }


        [Fact]
        public void CancelOrder_PendingOrder_CancelsOrderAndRestoresStock()
        {
            // Arrange
            var productRepository = new ProductRepository();
            var customerRepository = new CustomerRepository();

            var orderRepository = new OrderRepository(
                productRepository,
                customerRepository);

            var orderId =
                Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            var productId =
                Guid.Parse("33333333-3333-3333-3333-333333333333");

            var product = productRepository.GetProductById(productId);

            Assert.NotNull(product);

            int initialStock = product.StockQuantity;

            // Act
            orderRepository.CancelOrder(orderId);

            // Assert
            var order = orderRepository.GetOrderById(orderId);

            Assert.NotNull(order);
            Assert.Equal(OrderStatus.Cancelled, order.Status);
            Assert.Equal(initialStock + 3, product.StockQuantity);
        }

        [Fact]
        public void CancelOrder_InvalidOrderId_DoesNothing()
        {
            // Arrange
            var productRepository = new ProductRepository();
            var customerRepository = new CustomerRepository();

            var orderRepository = new OrderRepository(
                productRepository,
                customerRepository);

            var invalidOrderId = Guid.NewGuid();

            // Act
            orderRepository.CancelOrder(invalidOrderId);

            // Assert
            Assert.Null(orderRepository.GetOrderById(invalidOrderId));
        }


        [Fact]
        public void CancelOrder_CompletedOrder_DoesNothing()
        {
            // Arrange
            var productRepository = new ProductRepository();
            var customerRepository = new CustomerRepository();

            var orderRepository = new OrderRepository(
                productRepository,
                customerRepository);

            var orderId =
                Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            var productId =
                Guid.Parse("11111111-1111-1111-1111-111111111111");

            var product = productRepository.GetProductById(productId);

            Assert.NotNull(product);

            int initialStock = product.StockQuantity;

            // Act
            orderRepository.CancelOrder(orderId);

            // Assert
            var order = orderRepository.GetOrderById(orderId);

            Assert.NotNull(order);
            Assert.Equal(OrderStatus.Completed, order.Status);
            Assert.Equal(initialStock, product.StockQuantity);
        }


        [Fact]
        public void CancelOrder_AlreadyCancelledOrder_DoesNothing()
        {
            // Arrange
            var productRepository = new ProductRepository();
            var customerRepository = new CustomerRepository();

            var orderRepository = new OrderRepository(
                productRepository,
                customerRepository);

            var orderId =
                Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");

            var productId =
                Guid.Parse("44444444-4444-4444-4444-444444444444");

            var product = productRepository.GetProductById(productId);

            Assert.NotNull(product);

            int initialStock = product.StockQuantity;

            // Act
            orderRepository.CancelOrder(orderId);

            // Assert
            var order = orderRepository.GetOrderById(orderId);

            Assert.NotNull(order);
            Assert.Equal(OrderStatus.Cancelled, order.Status);
            Assert.Equal(initialStock, product.StockQuantity);
        }
    }
}
