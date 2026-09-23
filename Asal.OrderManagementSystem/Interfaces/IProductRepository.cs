using Asal.OrderManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asal.OrderManagementSystem.Interfaces
{
    public interface IProductRepository
    {
        Guid? GetProductById();
        List<Product> GetAll();

        Guid? AddProduct(string name,decimal price,int stockQuantity);

        bool AddProductToOrder(Guid orderId, Guid productId);
    }
}
