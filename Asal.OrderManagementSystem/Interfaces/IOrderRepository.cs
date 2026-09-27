using Asal.OrderManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asal.OrderManagementSystem.Interfaces
{
    public interface IOrderRepository
    {
        public Guid? CreateOrder(Guid customerId,List<OrderItem> items);

        public List<Order> GetAllOrders();

        public Order? GetOrderById(Guid orderId);

        public List<Order> GetOrdersByStatus(OrderStatus orderStatus);

        public void CancelOrder(Guid orderId);

        public decimal? CalculateOrderTotalPrice(Guid orderId);


        public bool AddProductToOrder(Guid orderId, Guid productId, int quantity);

    }
}
