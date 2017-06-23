using Organizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using Organizer.Helpers;

namespace Organizer.Controllers
{
    public class HomeController : Controller
    {
        OrganizerContext db = new OrganizerContext();
        DBHelper dbHelper = new DBHelper();
        public ActionResult Index()
        {
            IEnumerable<Diaryes> diaryes = db.Diary;
            ViewBag.Diary = diaryes;
            return View();
        }
        [HttpGet]
        public ActionResult DiaryAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DiaryAdd(Diaryes entity)
        {
            db.Diary.Add(entity);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DiaryDay()
        {
            IEnumerable<Diaryes> diaryes = db.Diary.Where(diary=>diary.BeginDate.Day==DateTime.Now.Day);
            ViewBag.Diary = diaryes;
            return View();
        }

        public ActionResult DiaryWeek()
        {
            DateTime now = DateTime.Now; // текущее время
            DateTime currentWeekStart = now.Date.AddDays(1 - (int)now.DayOfWeek); // дата начала текущей недели
            DateTime nextWeekStart = currentWeekStart.AddDays(7); // дата начала следующей недели
            // dateTime >= currentWeekStart && dateTime < nextWeekStart;

            SqlParameter param1 = new SqlParameter("@currentWeekStart", currentWeekStart);
            SqlParameter param2 = new SqlParameter("@nextWeekStart", nextWeekStart);

            IEnumerable<Diaryes> diaryes = db.Database.SqlQuery<Diaryes>("select * from Diaryes where BeginDate >= @currentWeekStart and BeginDate < @nextWeekStart",
                param1,param2);
            ViewBag.Diary = diaryes;
            return View();
        }

        public ActionResult DiaryMonth()
        {
            IEnumerable<Diaryes> diaryes = db.Diary.Where(diary=>diary.BeginDate.Month==DateTime.Now.Month);
            ViewBag.Diary = diaryes;
            return View();
        }
        protected bool WeekNumber(DateTime dateTime)
        {
            DateTime now = DateTime.Now; // текущее время
            DateTime currentWeekStart = now.Date.AddDays(1 - (int)now.DayOfWeek); // дата начала текущей недели
            DateTime nextWeekStart = currentWeekStart.AddDays(7); // дата начала следующей недели

            // покажет, принадлежит ли время к текущей неделе
            bool dateTimeIsOnCurrentWeek = dateTime >= currentWeekStart && dateTime < nextWeekStart;
            return dateTimeIsOnCurrentWeek;
        }
    }
}