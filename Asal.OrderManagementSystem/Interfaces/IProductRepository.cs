using Asal.OrderManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asal.OrderManagementSystem.Interfaces
{
    public interface IProductRepository
    {
        Product? GetProductById(Guid productId);
        List<Product> GetAll();

        Guid? AddProduct(string name,decimal price,int stockQuantity);

    }
}
