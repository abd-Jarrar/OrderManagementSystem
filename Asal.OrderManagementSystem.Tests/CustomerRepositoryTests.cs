using Asal.OrderManagementSystem.Models;
using Asal.OrderManagementSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asal.OrderManagementSystem.Tests
{
    public class CustomerRepositoryTests
    {
        [Fact]
        public void GetAllCustomers_Valid_ReturnsAllCustomers()
        {
            //arrange
            var cr=new CustomerRepository();

            //act
            var customers=cr.GetAllCustomers();

            //Assert
            Assert.NotNull(customers);
            Assert.Equal(3, customers.Count());
        }

        [Fact]
        public void GetCustomerById_null_ReturnsNull()
        {
            //arrange
            var cr = new CustomerRepository();

            //act
            var customer = cr.GetCustomerById(Guid.Parse("3f8a1c2e-7b4d-4f91-0000-9c5e8d1b2047"));

            //Assert

            Assert.Null(customer);
        }

        [Fact]
        public void GetCustomerById_ValidCustomer_ReturnCustomer()
        {
            //arrange
            var cr = new CustomerRepository();
            var expectedEmail = "abd@gmail.com";
            //act
            var customer = cr.GetCustomerById(Guid.Parse("3f8a1c2e-7b4d-4f91-a632-9c5e8d1b2047"));

            //Assert

            Assert.Equal(customer?.Email, expectedEmail);
        }
        [Fact]
        public void AddCustomer_ValidCustomer_ReturnsGuid()
        {
            //arrange
            var cr = new CustomerRepository();
            var newCustomerName = "abood";
            var newCustomerEmail = "abood@email.com";

            //act
            Guid? id=cr.AddCustomer("abood", "abood@email.com");

            //assert
            var customer = cr.GetCustomerById((Guid)id!);

            Assert.Equal(customer?.Name, newCustomerName);
            Assert.Equal(customer?.Email, newCustomerEmail);

        }


        [Fact]
        public void AddCustomer_EmptyName_ReturnsNull()
        {
            // Arrange
            var cr = new CustomerRepository();

            // Act
            Guid? id = cr.AddCustomer("", "abood@email.com");

            // Assert
            Assert.Null(id);
        }


        [Fact]
        public void AddCustomer_EmptyEmail_ReturnsNull()
        {
            // Arrange
            var cr = new CustomerRepository();

            // Act
            Guid? id = cr.AddCustomer("abood", "");

            // Assert
            Assert.Null(id);
        }

        [Fact]
        public void AddCustomer_DuplicateEmail_ReturnsNull()
        {
            // Arrange
            var cr = new CustomerRepository();
            var duplicatedEmail= "abd@gmail.com";
            // Act
            Guid? id = cr.AddCustomer(
                "another customer",
                duplicatedEmail);

            // Assert
            Assert.Null(id);
        }
    }
}
