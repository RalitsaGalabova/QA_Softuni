using System;
using Moq;
using NotificationApp.Entities;
using NotificationApp.Interfaces;
using NotificationApp.Services;
using NUnit.Framework;

namespace NotificationApp.Tests
{
    public class NotificationAppTests
    {
        private Mock<IUserRepository> _mockUserRepo;
        private Mock<INotifier> _mockNotifier;
        private NotificationService _notificationService;

        [SetUp]
        public void Setup()
        {
            _mockUserRepo = new Mock<IUserRepository>();
            _mockNotifier = new Mock<INotifier>();
            _notificationService = new NotificationService(_mockUserRepo.Object, _mockNotifier.Object);
        }

        [Test]
        public void NotifyUser_WithValidActiveUser_CallsSend()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                Email = "test@email.com",
                IsActive = true
            };

            // Act
            _mockUserRepo.Setup(repo => repo.GetUserById(user.Id)).Returns(user);
            _notificationService.NotifyUser(1, "hello");


            // Assert
            _mockNotifier.Verify(n => n.Send("test@email.com", "hello"), Times.Once);

        }

        [Test]
        public void NotifyUser_WithInactiveUser_ThrowsInvalidOperationException()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                Email = "test@email.com",
                IsActive = false
            };

            // Act
            _mockUserRepo.Setup(repo => repo.GetUserById(user.Id)).Returns(user);

            //Assert
            Assert.Throws<InvalidOperationException>(() => _notificationService.NotifyUser(1, "hello"));
            _mockNotifier.Verify(n => n.Send(It.IsAny<string>(), It.IsAny<string>()), Times.Never);

        }

        [Test]
        public void NotifyUser_WithNonExistentUser_ThrowsArgumentException()
        {
            _mockUserRepo.Setup(repo => repo.GetUserById(It.IsAny<int>())).Returns((User)null);

            //Assert
            Assert.Throws<ArgumentException>(() => _notificationService.NotifyUser(999, "hello"));
        }

        [Test]
        public void NotifyUser_WithEmptyMessage_ThrowsArgumentException()
        {
            // Arrange
            var user = new User
            {
                Id = 2,
                Email = "test2@email.com",
                IsActive = true
            };

            // Act
            _mockUserRepo.Setup(repo => repo.GetUserById(user.Id)).Returns(user);

            //Assert
            Assert.Throws<ArgumentException>(() => _notificationService.NotifyUser(2, ""));
        }
    }
}

