using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using NUnit.Framework;
using Xunit;
using ContactList.Controllers;
using Moq;
using ContactList.Interfaces;
using System.Data.Entity;
using ContactList.Models;
using System.Web.Mvc;

namespace ContactList.Tests
{
    public class HomeControllerTests
    {
        private HomeController _controller;
        private Mock<ApplicationDbContext> _context;

        public HomeControllerTests()
        {
            _context = new Mock<ApplicationDbContext>();
            _controller = new HomeController(_context.Object);
        }
        // index Action Result
        [Fact]
        public void HomeControllerIndex_returns_List()
        {
            //Arrange
            var mockSet = new Mock<DbSet<Contact>>();
            _context.Setup(m => m.Contacts).Returns(mockSet.Object);
            //Act
            var result = _controller.Index();
            //Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}
