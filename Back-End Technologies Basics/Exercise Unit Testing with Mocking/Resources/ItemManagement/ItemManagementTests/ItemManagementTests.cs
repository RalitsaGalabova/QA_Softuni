using NUnit.Framework;
using Moq;
using ItemManagementApp.Services;
using ItemManagementLib.Repositories;
using ItemManagementLib.Models;
using System.Collections.Generic;
using System.Linq;

namespace ItemManagement.Tests
{
    [TestFixture]
    public class ItemServiceTests
    {
        private Mock<IItemRepository> _mockRepository;
        private ItemService _itemService;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IItemRepository>();

            _itemService = new ItemService(_mockRepository.Object);

        }

        [Test]
        public void AddItem_ShouldCallAddItemOnRepository()
        {
            // Act
            _itemService.AddItem("TestItem");

            // Assert
            _mockRepository.Verify(r => r.AddItem(It.Is<Item>(i => i.Name == "TestItem")), Times.Once);

        }

        [Test]
        public void GetAllItems_ShouldReturnAllItems()
        {
            // Arrange
            var mockItems = new List<Item>
            {
                new Item { Id = 1, Name = "Item1" },
                new Item { Id = 2, Name = "Item2" }
            };

            // Act
            _mockRepository.Setup(r => r.GetAllItems()).Returns(mockItems);
            var result = _itemService.GetAllItems();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));

        }

        [Test]
        public void UpdateItem_ShouldCallUpdateItemOnRepository()
        {
            // Arrange
            var existingItem = new Item { Id = 1, Name = "OldName" };
            _mockRepository.Setup(r => r.GetItemById(1)).Returns(existingItem);
            // Act
            _itemService.UpdateItem(1, "NewName");
            // Assert
            _mockRepository.Verify(r => r.UpdateItem(It.Is<Item>(i => i.Id == 1 && i.Name == "NewName")), Times.Once);
        }

        [Test]
        public void DeleteItem_ShouldCallDeleteItemOnRepository()
        {
            // Act
            _itemService.DeleteItem(1);
            // Assert
            _mockRepository.Verify(r => r.DeleteItem(1), Times.Once);
        }

        [Test]
        public void ValidateItemName_ShouldReturnTrueForValidName()
        {
            // Act
            var isValid = _itemService.ValidateItemName("ValidName");
            // Assert
            Assert.That(isValid, Is.True);
        }
    }
}