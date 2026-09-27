using Asal.OrderManagementSystem.Interfaces;
using Asal.OrderManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asal.OrderManagementSystem.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new()
    {
        new Product
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Name = "Laptop",
            Price = 1200m,
            StockQuantity = 10
        },
        new Product
        {
            Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            Name = "Keyboard",
            Price = 75m,
            StockQuantity = 25
        },
        new Product
        {
            Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
            Name = "Mouse",
            Price = 40m,
            StockQuantity = 50
        },
        new Product
        {
            Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
            Name = "Monitor",
            Price = 300m,
            StockQuantity = 15
        }
    };
        public Guid? AddProduct(string name, decimal price, int stockQuantity)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;
            if (stockQuantity < 0||price<0)
                return null;
            var newProduct=new Product {
                Id = Guid.NewGuid(),
                Name = name,
                Price = price,
                StockQuantity = stockQuantity 
            };
            _products.Add(newProduct);
            return newProduct.Id;
        }


        public List<Product> GetAll()
        {
            return _products.ToList();
        }

        public Product? GetProductById(Guid productId)
        {
            return _products.FirstOrDefault(p=>p.Id == productId);
        }
    }
}
