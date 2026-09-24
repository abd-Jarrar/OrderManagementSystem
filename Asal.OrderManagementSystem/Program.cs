using Asal.OrderManagementSystem.Repositories;
using Asal.OrderManagementSystem.Services;

namespace Asal.OrderManagementSystem;

public class Program
{
    public static void Main()
    {
        // Repositories
        var customerRepository = new CustomerRepository();
        var productRepository = new ProductRepository();

        var orderRepository = new OrderRepository(
            productRepository,
            customerRepository);

        // Services
        var customerService = new CustomerService(customerRepository);
        var productService = new ProductService(productRepository);

        var orderService = new OrderService(
            orderRepository,
            productRepository,
            customerRepository);

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("========================================");
            Console.WriteLine("       ORDER MANAGEMENT SYSTEM");
            Console.WriteLine("========================================");
            Console.WriteLine("1.  Add Customer");
            Console.WriteLine("2.  View Customers");
            Console.WriteLine("3.  Add Product");
            Console.WriteLine("4.  View Products");
            Console.WriteLine("5.  Create Order");
            Console.WriteLine("6.  Add Product to Order");
            Console.WriteLine("7.  View Order Details");
            Console.WriteLine("8.  View Orders");
            Console.WriteLine("9.  Filter Orders by Status");
            Console.WriteLine("10. Calculate Order Total");
            Console.WriteLine("11. Cancel Order");
            Console.WriteLine("12. Exit");
            Console.WriteLine("========================================");

            Console.Write("Enter your choice: ");
            string? choice = Console.ReadLine();

            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    customerService.AddCustomer();
                    break;

                case "2":
                    customerService.PrintAllCustomers();
                    break;

                case "3":
                    productService.AddProduct();
                    break;

                case "4":
                    productService.PrintAllProducts();
                    break;

                case "5":
                    orderService.CreateOrder();
                    break;

                case "6":
                    orderService.AddProductToOrder();
                    break;

                case "7":
                    orderService.DisplayOrder();
                    break;

                case "8":
                    orderService.DisplayAllOrders();
                    break;

                case "9":
                    orderService.FilterOrdersByStats();
                    break;

                case "10":
                    orderService.CalculateOrderTotal();
                    break;

                case "11":
                    orderService.CancelOrder();
                    break;

                case "12":
                    Console.WriteLine("Goodbye!");
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please choose 1-12.");
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}