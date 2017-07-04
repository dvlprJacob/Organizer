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
        [HttpGet]
        public ActionResult ContactSearch()
        {
            List<string> items = new List<string>() { "Имя", "Фамилия", "Отчество", "День рождения", "Организация", "Должность", "Контакты" };
            ViewData["colName"] = new SelectList(items);
            return View();
        }
        [HttpPost]
        public ActionResult ContactSearch(string colName, string srchValue)
        {
            List<string> items = new List<string>() { "Имя", "Фамилия", "Отчество", "День рождения (дд.мм.гггг)", "Организация", "Должность", "Контакты" };
            ViewData["colName"] = new SelectList(items);
            IEnumerable<ContactList> contacts;

            switch (colName)
            {
                case "Имя":
                    contacts = db.Contact.Where(c => c.Name.Contains(srchValue));
                    return View(contacts.ToList<ContactList>());
                case "Фамилия":
                    contacts = db.Contact.Where(c => c.Surname.Contains(srchValue));
                    return View(contacts.ToList<ContactList>());
                case "Отчество":
                    contacts = db.Contact.Where(c => c.Patronymic.Contains(srchValue));
                    return View(contacts.ToList<ContactList>());
                case "Организация":
                    contacts = db.Contact.Where(c => c.Organization.Contains(srchValue));
                    return View(contacts.ToList<ContactList>());
                case "Должность":
                    contacts = db.Contact.Where(c => c.Position.Contains(srchValue));
                    return View(contacts.ToList<ContactList>());
                case "Контакты":
                    contacts = db.Contact.Where(c => c.Contacts.Contains(srchValue));
                    return View(contacts.ToList<ContactList>());
                case "День рождения(дд.мм.гггг)":
                    string[] itms = srchValue.Split('.');
                    contacts = db.Contact.Where(c => c.BirthDate.ToString().Contains(srchValue));
                    return View(contacts.ToList<ContactList>());
                default:
                    return View();
            }
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
        [HttpGet]
        public ActionResult ContactDelete(int id)
        {
            ContactList contact = db.Contact.Find(id);
            if (contact != null)
                return View(contact);
            else
                return HttpNotFound();
        }
        [HttpPost,ActionName("ContactDelete")]
        public ActionResult ContactDeleteConfirmed(int id)
        {
            ContactList contact = db.Contact.Find(id);
            if (contact == null)
                return HttpNotFound();
            db.Contact.Remove(contact);
            db.SaveChanges();
            return RedirectToAction("Index", "ContactList");
        }
    }
}