using Asal.OrderManagementSystem.Interfaces;
using Asal.OrderManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asal.OrderManagementSystem.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders = new()
        {
        new Order
                {
                    Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    CustomerId = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    Status = OrderStatus.Completed,
                    CreatedDate = new DateTime(2026, 9, 20),
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ProductId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                            Quantity = 1,
                            UnitPrice = 1200m
                        },
                        new OrderItem
                        {
                            ProductId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                            Quantity = 2,
                            UnitPrice = 75m
                        }
                    }
                },

                new Order
                {
                    Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    CustomerId = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                    Status = OrderStatus.Pending,
                    CreatedDate = new DateTime(2026, 9, 21),
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ProductId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                            Quantity = 3,
                            UnitPrice = 40m
                        }
                    }
                },

                new Order
                {
                    Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                    CustomerId = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    Status = OrderStatus.Cancelled,
                    CreatedDate = new DateTime(2026, 9, 22),
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ProductId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                            Quantity = 1,
                            UnitPrice = 300m
                        }
                    }
                }
            };
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        public OrderRepository(IProductRepository productRepository,ICustomerRepository customerRepository)
        {
            _productRepository=productRepository;
            _customerRepository = customerRepository;
        }

        public bool AddProductToOrder(Guid orderId, Guid productId, int quantity)
        {
            var order = GetOrderById(orderId);

            if (order is null)
                return false;

            var product = _productRepository.GetProductById(productId);

            if (product is null)
                return false;

            if (quantity <= 0)
                return false;

            if (product.StockQuantity < quantity)
                return false;

            var existingItem = order.OrderItems
                .FirstOrDefault(item => item.ProductId == productId);

            if (existingItem is not null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    UnitPrice = product.Price
                });
            }

            product.StockQuantity -= quantity;

            return true;
        }
        public decimal? CalculateOrderTotalPrice(Guid orderId)
        {
            var order = _orders.FirstOrDefault(o => o.Id == orderId);
            if (order is null)
                return null;
            var total = order.OrderItems.Sum(oi => oi.UnitPrice * oi.Quantity);
            return total;

        }

        public void CancelOrder(Guid orderId)
        {
            var order = GetOrderById(orderId);

            if (order is null)
                return;

            if ((order.Status == OrderStatus.Cancelled)||(order.Status == OrderStatus.Completed))
                return;

            foreach (var item in order.OrderItems)
            {
                var product = _productRepository.GetProductById(item.ProductId);

                if (product is not null)
                {
                    product.StockQuantity += item.Quantity;
                }
            }
            order.Status = OrderStatus.Cancelled;

        }

        public Guid? CreateOrder(Guid customerId, List<OrderItem> items)
        {
            var customer = _customerRepository.GetCustomerById(customerId);

            if (customer is null)
                return null;

            if (items is null || items.Count == 0)
                return null;

            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                Status = OrderStatus.Pending,
                CreatedDate = DateTime.Now,
                OrderItems = items
            };

            _orders.Add(order);

            return order.Id;
        }

        public List<Order> GetAllOrders()
        {
            return _orders.ToList();
        }

        public Order? GetOrderById(Guid orderId)
        {
            return _orders.FirstOrDefault(o => o.Id == orderId);
        }

        public List<Order> GetOrdersByStatus(OrderStatus orderStatus)
        {
            return _orders.Where(o=>o.Status== orderStatus).ToList();
        }
    }
}
