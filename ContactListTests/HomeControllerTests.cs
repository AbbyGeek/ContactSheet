using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using NUnit.Framework;
//using Xunit;
using ContactList.Controllers;
using Moq;
using ContactList.Interfaces;
using System.Data.Entity;
using ContactList.Models;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ContactList.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        private HomeController _controller;
        private Mock<IApplicationDbContext> _context;

        public HomeControllerTests()
        {
            _context = new Mock<IApplicationDbContext>();
            _controller = new HomeController(_context.Object);
        }
        // index Action Result
        [TestMethod]
        public void HomeControllerIndex_returns_List()
        {
            //Arrange
            var data = new List<Contact>
            {
                new Contact { firstName="XXX", lastName="XXX", address="XXX", company="DDD", email="email@gmail.com", primaryPhone="1231231234", id= 1},
                new Contact { firstName="XXX", lastName="XXX", address="XXX", company="DDD", email="email@gmail.com", primaryPhone="1231231234", id= 1},
                new Contact { firstName="XXX", lastName="XXX", address="XXX", company="DDD", email="email@gmail.com", primaryPhone="1231231234", id= 1}
            }.AsQueryable();
            var mockSet = new Mock<DbSet<Contact>>();
            mockSet.As<IQueryable<Contact>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Contact>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Contact>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Contact>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _context.Setup(m => m.Contacts).Returns(mockSet.Object);
            //Act
            var result = _controller.Index();
            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
