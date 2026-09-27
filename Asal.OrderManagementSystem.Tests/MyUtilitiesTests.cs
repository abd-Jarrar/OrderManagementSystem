using Asal.OrderManagementSystem.Utilities;
using System.Security;
using System.Security.Cryptography.X509Certificates;

namespace Asal.OrderManagementSystem.Tests;

public class MyUtilitiesTests
{
    [Fact]
    public void ReadGuid_ValidInput_ReturnsGuid()
    {

        //Arrange
        var expectedId = Guid.Parse("55555555-5555-5555-5555-555555555555");

        var input = new StringReader("55555555-5555-5555-5555-555555555555");

        Console.SetIn(input);

        //Act
        Guid actualGuid = MyUtilities.ReadGuid("Enter ID: ");


        //Assert
        Assert.Equal(expectedId, actualGuid);




        //public static Guid ReadGuid(string message)
        //{
        //    while (true)
        //    {
        //        Console.Write(message);
        //        string? input = Console.ReadLine();

        //        if (Guid.TryParse(input, out Guid id))
        //            return id;

        //        Console.WriteLine("Invalid GUID. Please try again.");
        //    }
        //}






    }
    [Fact]
    public void ReadGuid_InvalidInput_PrintsError()
    {
        // Arrange
        var input = new StringReader(
            "invalid\n55555555-5555-5555-5555-555555555555"
        );

        var output = new StringWriter();

        Console.SetIn(input);
        Console.SetOut(output);

        // Act
        MyUtilities.ReadGuid("Enter ID: ");

        // Assert
        Assert.Contains(
            "Invalid GUID. Please try again.",
            output.ToString()
        );
    }

    
}
