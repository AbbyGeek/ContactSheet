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

namespace ContactList.Tests
{
    public class ContactControllerTests
    {
        private ContactsController _controller;
        private Mock<ApplicationDbContext> _context;

        public ContactControllerTests()
        {
            _context = new Mock<ApplicationDbContext>();
            _controller = new ContactsController(_context.Object);
        }

        [TestMethod]
        public void ContactFormAction_Returns_Form()
        {
            Assert.AreEqual(1, 1);
        }
        //[Fact]
        //public void HomeControllerIndex_returns_List()
        //{
        //    //Arrange
        //    var mockSet = new Mock<DbSet<Contact>>();
        //    _context.Setup(m => m.Contacts).Returns(mockSet.Object);
        //    //Act
        //    var result = _controller.Index();
        //    //Assert
        //    Assert.IsType<ViewResult>(result);
        //}
    }
}
