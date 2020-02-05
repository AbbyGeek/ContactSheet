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
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Index()
        {
            var contacts = _context.Contacts.ToList();
            return View(contacts);
        }

        public ActionResult AddContact()
        {
            ViewBag.Message = "Add Contact";
            return View();
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
    }
}