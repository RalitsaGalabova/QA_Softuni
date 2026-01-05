using NUnit.Framework;
using System;

namespace TestApp.Tests;

public class LongestIncreasingSubsequenceTests
{
    [Test]
    public void Test_GetLis_NullArray_ThrowsArgumentNullException()
    {
        //Arrange
        int[] array = null;

        //Act, Assert
        Assert.Throws<ArgumentNullException>(() => LongestIncreasingSubsequence.GetLis(array));
    }

    [Test]
    public void Test_GetLis_EmptyArray_ReturnsEmptyString()
    {
        //Arrange
        int[] array = Array.Empty<int>();
        string expected = string.Empty; //""

        //Act
        string result = LongestIncreasingSubsequence.GetLis(array);


        //Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Test_GetLis_SingleElementArray_ReturnsElement()
    {
        //Arrange
        int[] array = new int[] { 1 };
        string expected = "1";

        //Act
        string result = LongestIncreasingSubsequence.GetLis(array);


        //Assert
        Assert.That(result, Is.EqualTo(expected));
    }


    [Test]
    public void Test_GetLis_UnsortedArray_ReturnsLongestIncreasingSubsequence()
    {
        //Arrange
        int[] array = new int[] { 10, 9, 2, 5, 3, 7, 101, 18 };
        string expected = "2 5 7 101";

        //Act
        string result = LongestIncreasingSubsequence.GetLis(array);


        //Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Test_GetLis_SortedArray_ReturnsItself()
    {
        //Arrange
        int[] array = new int[] { 1, 2, 3, 4, 5};
        string expected = "1 2 3 4 5";

        //Act
        string result = LongestIncreasingSubsequence.GetLis(array);


        //Assert
        Assert.That(result, Is.EqualTo(expected));
    }
}
