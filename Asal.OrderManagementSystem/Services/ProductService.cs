using Asal.OrderManagementSystem.Interfaces;
using Asal.OrderManagementSystem.Models;
using Asal.OrderManagementSystem.Repositories;
using Asal.OrderManagementSystem.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asal.OrderManagementSystem.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void AddProduct()
        {
            string name = MyUtilities.Read("Enter product name: ");

            decimal price = MyUtilities.ReadPositiveAmount("Enter product price: ");

            int stockQuantity;

            while (true)
            {
                string input = MyUtilities.Read("Enter stock quantity: ");

                if (int.TryParse(input, out stockQuantity) && stockQuantity >= 0)
                    break;

                Console.WriteLine("Invalid stock quantity. Please enter a valid number.");
            }

            Guid? productId = _productRepository.AddProduct(
                name,
                price,
                stockQuantity
            );

            if (productId is null)
            {
                throw new InvalidOperationException("Failed to add product.");
            }

            Console.WriteLine($"Product added successfully. Id: {productId}");
        }

        
        public void PrintAllProducts()
        {
            List<Product> products = _productRepository.GetAll();

            if (products.Count == 0)
            {
                Console.WriteLine("No products found.");
                return;
            }

            MyUtilities.PrintProducts(products);
        }
    }
}
