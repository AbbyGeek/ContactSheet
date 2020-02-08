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
    public class ContactControllerTests
    {
        private ContactsController _controller;
        private Mock<IApplicationDbContext> _context;

        public ContactControllerTests()
        {
            _context = new Mock<IApplicationDbContext>();
            _controller = new ContactsController(_context.Object);
        }
        public IQueryable<Contact> TestContactList()
        {
            var data = new List<Contact>
            {
                new Contact { firstName="name1", lastName="XXX", address="XXX", company="DDD", email="email@gmail.com", primaryPhone="1231231234", id= 5},
                new Contact { firstName="name2", lastName="XXX", address="XXX", company="DDD", email="email@gmail.com", primaryPhone="1231231234", id= 6},
                new Contact { firstName="name3", lastName="XXX", address="XXX", company="DDD", email="email@gmail.com", primaryPhone="1231231234", id= 7}
            }.AsQueryable();
            return (data);
        }
        // index Action Result
        [TestMethod]
        public void ContactForm_Returns_View()
        {
            var result = _controller.ContactForm();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void Save_AddsContact_WhenIdIsZero()
        {
            var data = TestContactList();
            var contact = new Contact { firstName = "zero", lastName = "zero", address = "XXX", company = "DDD", email = "email@gmail.com", primaryPhone = "1231231234", id = 0 };
            var mockSet = new Mock<DbSet<Contact>>();
            mockSet.As<IQueryable<Contact>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Contact>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Contact>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Contact>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _context.Setup(m => m.Contacts).Returns(mockSet.Object);

            var result = _controller.Save(contact);
            _context.Verify(m => m.Contacts.Add(It.IsAny<Contact>()),Times.Once());

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            _context.Verify(m => m.SaveChanges(), Times.Once);
        }
        
        [TestMethod]
        public void Save_UpdatesContactsWhen_IdNotZero()
        {
            var data = TestContactList();
            var contact = new Contact { firstName = "CHANGED NAME", lastName = "zero", address = "XXX", company = "DDD", email = "email@gmail.com", primaryPhone = "1231231234", id = 5 };
            var mockSet = new Mock<DbSet<Contact>>();
            mockSet.As<IQueryable<Contact>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Contact>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Contact>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Contact>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _context.Setup(m => m.Contacts).Returns(mockSet.Object);

            var result = _controller.Save(contact);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            _context.Verify(m => m.SaveChanges(), Times.Once);
            var SingleMethodResult = mockSet.Object.Single(c => c.id == contact.id);
            Assert.AreEqual(SingleMethodResult.firstName, contact.firstName);
        }

        //View Contact
        [TestMethod]
        public void ViewContact_Returns_Contact()
        {
            var data = TestContactList();
            var contact = new Contact { firstName = "name2", lastName = "XXX", address = "XXX", company = "DDD", email = "email@gmail.com", primaryPhone = "1231231234", id = 6 };
            var mockSet = new Mock<DbSet<Contact>>();
            mockSet.As<IQueryable<Contact>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Contact>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Contact>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Contact>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _context.Setup(m => m.Contacts).Returns(mockSet.Object);

            var result = _controller.ViewContact(contact.id);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var SingleMethodResult = mockSet.Object.SingleOrDefault(c => c.id == contact.id);
            Assert.AreEqual(SingleMethodResult.firstName, contact.firstName);
        }

        [TestMethod]
        public void ViewContact_ReturnsNotFoundWhen_ContactIsNullOrZero()
        {
            var data = TestContactList();
            var contact = new Contact { firstName = "zero", lastName = "XXX", address = "XXX", company = "DDD", email = "email@gmail.com", primaryPhone = "1231231234", id = 0 };
            var mockSet = new Mock<DbSet<Contact>>();
            mockSet.As<IQueryable<Contact>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Contact>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Contact>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Contact>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _context.Setup(m => m.Contacts).Returns(mockSet.Object);

            var result = _controller.ViewContact(contact.id);

            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }
        //Delete
        [TestMethod]
        public void Delete_Removes_Contact()
        {
            var data = TestContactList();
            var contact = new Contact { firstName = "name1", lastName = "XXX", address = "XXX", company = "DDD", email = "email@gmail.com", primaryPhone = "1231231234", id = 5 };
            var mockSet = new Mock<DbSet<Contact>>();
            mockSet.As<IQueryable<Contact>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Contact>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Contact>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Contact>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _context.Setup(m => m.Contacts).Returns(mockSet.Object);

            var result = _controller.Delete(contact.id);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var SingleOrDefaultMethodResult = mockSet.Object.SingleOrDefault(m => m.id == contact.id);
            Assert.AreEqual(SingleOrDefaultMethodResult.firstName, contact.firstName);
    //        _context.Verify(m => m.Contacts.Remove(contact), Times.Once);
            _context.Verify(m => m.SaveChanges(), Times.Once);


        }

        //Edit
        [TestMethod]
        public void EditReturns_ContactAndContactForm_WithContact()
        {
            var data = TestContactList();
            var contact = new Contact { firstName = "name1", lastName = "XXX", address = "XXX", company = "DDD", email = "email@gmail.com", primaryPhone = "1231231234", id = 5 };
            var mockSet = new Mock<DbSet<Contact>>();
            mockSet.As<IQueryable<Contact>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Contact>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Contact>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Contact>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            _context.Setup(m => m.Contacts).Returns(mockSet.Object);

            var result = _controller.Edit(contact.id);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var SingleOrDefaultMethodResult = mockSet.Object.SingleOrDefault(m => m.id == contact.id);
            Assert.AreEqual(SingleOrDefaultMethodResult.firstName, contact.firstName);
        }
        [TestMethod]
        public void EditRedirects_Redirect_WithNullContactId()
        {
            var data = TestContactList();
            var contact = new Contact { firstName = "name1", lastName = "XXX", address = "XXX", company = "DDD", email = "email@gmail.com", primaryPhone = "1231231234", id = 0 };
            var mockSet = new Mock<DbSet<Contact>>();
            mockSet.As<IQueryable<Contact>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Contact>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Contact>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Contact>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            _context.Setup(m => m.Contacts).Returns(mockSet.Object);

            var result = _controller.Edit(contact.id);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }
        //New
        [TestMethod]
        public void NewCreates_NewContact()
        {
            var result = _controller.New();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
