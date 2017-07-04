﻿using Organizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;


namespace Organizer.Controllers
{
    public class ContactListController : Controller
    {
        ContactListContext db = new ContactListContext();
        // GET: Contact
        public ActionResult Index()
        {
            IEnumerable<ContactList> con = db.Contact;
            return View(con.ToList<ContactList>());
        }
        [HttpGet]
        public ActionResult ContactUpdate(int id)
        {
            ContactList diary = db.Contact.Find(id);
            return View(diary);
        }
        [HttpPost]
        public ActionResult ContactUpdate(ContactList diary)
        {
            db.Entry(diary).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index","ContactList");
        }
        [HttpGet]
        public ActionResult ContactAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ContactAdd(ContactList entity)
        {
            db.Contact.Add(entity);
            db.SaveChanges();
            return RedirectToAction("Index","ContactList");
        }
    }
}