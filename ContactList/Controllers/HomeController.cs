using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContactList.Models;

namespace ContactList.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var contacts = GetContacts();
            return View(contacts);
        }

        private IEnumerable<Contact> GetContacts()
        {
            return new List<Contact>
            {
                new Contact() {id=1,firstName = "John", lastName="Smith", company="Working Company", address="123 home lane", email="ab@123.com", primaryPhone=1234432343, secondaryPhone=5555555555},
                new Contact() {id=2, firstName = "John", lastName="Smith II", company="Working Company", address="123 home lane", email="ab@123.com", primaryPhone=1234432343, secondaryPhone=5555555555},
                new Contact() {id=3, firstName = "John", lastName="Smith III", company="Working Company", address="123 home lane", email="ab@123.com", primaryPhone=1234432343, secondaryPhone=5555555555}
            };
        }


        public ActionResult AddContact()
        {
            ViewBag.Message = "Add Contact";
            return View();
        }

        public ActionResult ViewContact(int id)
        {
            var contact = GetContacts().SingleOrDefault(c => c.id == id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }
    }
}