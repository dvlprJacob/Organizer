using Organizer.Models;
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
    }
}