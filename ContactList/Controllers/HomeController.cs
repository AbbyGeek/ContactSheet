﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContactList.Interfaces;
using ContactList.Models;

namespace ContactList.Controllers
{
    public class HomeController : Controller
    {
        private IApplicationDbContext _context;
        public HomeController(IApplicationDbContext context)
        {
            _context = context;
        }

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var contacts = _context.Contacts.ToList();
            if (TempData["successMessage"] != null)
            {
                ViewBag.Message = TempData["successMessage"].ToString();
            }
            return View(contacts);
        }
    }
}