using System;
using NUnit.Framework;

namespace TestApp.Tests;

public class PascalTriangleTests
{
    //задачата решена, за да се провери от jusge
    [TestCase(0, "")]
    [TestCase(1, "1 \n")]
    [TestCase(2, "1 \n1 1 \n")]
    [TestCase(3, "1 \n1 1 \n1 2 1 \n")]
    [TestCase(4, "1 \n1 1 \n1 2 1 \n1 3 3 1 \n")]

    public void Test_PrintTriangle_ShouldReturnCorrectString(int n, string expected)
    {
      
        //Act
        string result = PascalTriangle.PrintTriangle(n);

        //Assert
        Assert.That(result, Is.EqualTo(expected));
    }


    /* Задачата решена с тестове в познат формат
    [Test]
    public void Test_PrintTriangle0_ShouldReturnCorrectString()
    {
        //Arrange
        string expected = "";

        //Act
        string result = PascalTriangle.PrintTriangle(0);

        //Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Test_PrintTriangle1_ShouldReturnCorrectString()
    {
        //Arrange
        string expected = "1 \n";

        //Act
        string result = PascalTriangle.PrintTriangle(1);

        //Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Test_PrintTriangle2_ShouldReturnCorrectString()
    {
        //Arrange
        string expected = "1 \n1 1 \n";

        //Act
        string result = PascalTriangle.PrintTriangle(2);

        //Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Test_PrintTriangle3_ShouldReturnCorrectString()
    {
        //Arrange
        string expected = "1 \n1 1 \n1 2 1 \n";

        //Act
        string result = PascalTriangle.PrintTriangle(3);

        //Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Test_PrintTriangle4_ShouldReturnCorrectString()
    {
        //Arrange
        string expected = "1 \n1 1 \n1 2 1 \n1 3 3 1 \n";

        //Act
        string result = PascalTriangle.PrintTriangle(4);

        //Assert
        Assert.That(result, Is.EqualTo(expected));
    }*/
}
