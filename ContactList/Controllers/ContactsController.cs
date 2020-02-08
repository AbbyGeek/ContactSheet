using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContactList.Models;

namespace ContactList.Controllers
{
    public class ContactsController : Controller
    {
        private ApplicationDbContext _context;

        public ContactsController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        //public ActionResult Index()
        //{
        //    var contacts = _context.Contacts.ToList();
        //    return View(contacts);
        //}

        public ActionResult ContactForm()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Save(Contact contact)
        {

            if(!ModelState.IsValid)
            {
                return View("ContactForm", contact);
            }
            if (contact.id == 0)
            {
                _context.Contacts.Add(contact);
            }
            else
            {
                var contactInDb = _context.Contacts.Single(c => c.id == contact.id);

                contactInDb.firstName = contact.firstName;
                contactInDb.lastName = contact.lastName;
                contactInDb.company = contact.company;
                contactInDb.email = contact.email;
                contactInDb.primaryPhone = contact.primaryPhone;
                contactInDb.secondaryPhone = contact.secondaryPhone;
                contactInDb.address = contact.address;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ViewContact(int id)
        {
            var contact = _context.Contacts.SingleOrDefault(c => c.id == id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        public ActionResult Delete(int id)
        {
            var contact = _context.Contacts.SingleOrDefault(c => c.id == id);
            _context.Contacts.Remove(contact);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit(int id)
        {
            var contact = _context.Contacts.SingleOrDefault(c => c.id == id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View("ContactForm", contact);
        }

        public ActionResult New()
        {
            var ViewModel = new Contact();
            return View("ContactForm", ViewModel);
        }
    }
}