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
using System.Data.Entity.Core.Objects;

namespace Organizer.Controllers
{
public class HomeController : Controller
    {
        OrganizerContext db = new OrganizerContext();
        public ActionResult Index()
        {
            var colList = new List<string>() { "Тип", "Начало", "Конец" };
            var colListSrch = new List<string>() { "Тип", "Тема", "Место","Начало","Конец"};
            var typesList = new List<string>() { "Встреча", "Дело", "Памятка" };

            ViewData["colNameF"] = new SelectList(colList);
            ViewData["colNameS"] = new SelectList(colListSrch);
            ViewData["filterType"] = new SelectList(typesList);

            IEnumerable<Diaryes> diaryes = db.Diary;
            return View(diaryes.ToList<Diaryes>());
        }
        private List<Diaryes> DiarySearch(string colName,string srchValue)
        {
            if (!string.IsNullOrEmpty(srchValue))
            {
                switch (colName)
                {
                    case "Тип":
                        var diary1 = from d in db.Diary where d.Type.Contains(srchValue) select d;
                        return diary1.ToList();
                    case "Тема":
                        var diary2 = from d in db.Diary where d.Theme.Contains(srchValue) select d;
                        return diary2.ToList();
                    case "Место":
                        var diary3 = from d in db.Diary where d.Place.Contains(srchValue) select d;
                        return diary3.ToList();
                    case "Начало":
                        var diary4 = from d in db.Diary where d.BeginDate.ToString("dd.mm.yyyy").Contains(srchValue) select d;
                        return diary4.ToList();
                    case "Конец":
                        var diary5 = from d in db.Diary where d.EndDate.Value.ToString("dd.mm.yyyy").Contains(srchValue) select d;
                        return diary5.ToList();
                }
            }
            return null;
        }
        [HttpPost]
        public ActionResult Index(string colNameF, string filterType, string filterDate,string srchValue,string colNameS)
        {
            var colList = new List<string>() { "Тип", "Начало", "Конец" };
            var typesList = new List<string>() { "Встреча", "Дело", "Памятка" };
            var colListSrch = new List<string>() { "Тип", "Тема", "Место","Начало","Конец" };

            ViewData["colNameS"] = new SelectList(colListSrch);
            ViewData["colNameF"] = new SelectList(colList);
            ViewData["filterType"] = new SelectList(typesList);

            if(!String.IsNullOrEmpty(srchValue))
            {
                return View(DiarySearch(colNameS, srchValue));
            }

            switch(colNameF)
                {
                case "Тип":
                    {

                        if (!string.IsNullOrEmpty(filterType))
                        {
                            var linqQuery = from d in db.Diary select d;
                            linqQuery = linqQuery.Where(d => d.Type.Contains(filterType)).OrderBy(d=>d.BeginDate);
                            return View(linqQuery.ToList());
                        }
                        break;
                    }
                case "Начало":
                    {
                            var linqQuery = from d in db.Diary select d;
                            // 'Date' is not supported LINQ to Entytyes' - не работает
                            linqQuery = linqQuery.Where(d => d.BeginDate.Date.ToString("dd.mm.yyyy").Contains(filterDate)).OrderBy(d=>d.BeginDate);
                            return View(linqQuery.ToList()); 
                    }
                case "Конец":
                    {
                            // 'Date' is not supported LINQ to Entytyes' - не работает
                            var linqQuery = from d in db.Diary select d;
                            linqQuery = linqQuery.Where(d => d.EndDate.Value.Date.ToString("dd.mm.yyyy").Contains(filterDate)).OrderBy(d => d.BeginDate);
                            return View(linqQuery.ToList());
                    }
            }
            return View();
        }
        [HttpGet]
        public ActionResult DiaryDone(int id)
        {
            Diaryes diary = db.Diary.Find(id);
            DiaryDone(diary);
            return RedirectToAction("Index");
        }
        private void DiaryDone(Diaryes diary)
        {
            db.Entry(diary).State = EntityState.Modified;
            diary.DoneStatus = 1;
            db.SaveChanges();
        }
        [HttpGet]
        public ActionResult DiaryUpdate(int id)
        {
            Diaryes diary = db.Diary.Find(id);
            return View(diary);
        }
        [HttpPost]
        public ActionResult DiaryUpdate(Diaryes diary)
        {
            db.Entry(diary).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult DiaryDelete(int id)
        {
            Diaryes diary = db.Diary.Find(id);
            if(diary==null)
            {
                return HttpNotFound();
            }
            return View(diary);
        }
        [HttpPost,ActionName("DiaryDelete")]
        public ActionResult DiaryDeleteConfirmed(int id)
        {
            Diaryes diary = db.Diary.Find(id);
            if (diary == null)
            {
                return HttpNotFound();
            }
            db.Diary.Remove(diary);
            db.SaveChanges();
            return RedirectToAction("Index");
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
            var colList = new List<string>() { "Тип", "Начало", "Конец" };
            ViewData["colName"] = new SelectList(colList);

            IEnumerable<Diaryes> diaryes = db.Diary.Where(diary=>diary.BeginDate.Day==DateTime.Now.Day);
            ViewBag.Diary = diaryes;
            return View();
        }

        public ActionResult DiaryWeek()
        {
            var colList = new List<string>() { "Тип", "Начало", "Конец" };
            ViewData["colName"] = new SelectList(colList);

            DateTime now = DateTime.Now; // текущее время
            DateTime currentWeekStart = now.Date.AddDays(1 - (int)now.DayOfWeek); // дата начала текущей недели
            DateTime nextWeekStart = currentWeekStart.AddDays(7); // дата начала следующей недели

            SqlParameter param1 = new SqlParameter("@currentWeekStart", currentWeekStart);
            SqlParameter param2 = new SqlParameter("@nextWeekStart", nextWeekStart);

            IEnumerable<Diaryes> diaryes = db.Database.SqlQuery<Diaryes>("select * from Diaryes where BeginDate >= @currentWeekStart and BeginDate < @nextWeekStart",
                param1,param2);
            ViewBag.Diary = diaryes;
            return View();
        }

        public ActionResult DiaryMonth()
        {
            var colList = new List<string>() { "Тип", "Начало", "Конец" };
            ViewData["colName"] = new SelectList(colList);

            IEnumerable<Diaryes> diaryes = db.Diary.Where(diary=>diary.BeginDate.Month==DateTime.Now.Month);
            ViewBag.Diary = diaryes;
            return View();
        }
        protected bool WeekNumber(DateTime dateTime)
        {
            DateTime now = DateTime.Now; // текущее время
            DateTime currentWeekStart = now.Date.AddDays(1 - (int)now.DayOfWeek); // дата начала текущей недели
            DateTime nextWeekStart = currentWeekStart.AddDays(7); // дата начала следующей недели
            bool dateTimeIsOnCurrentWeek = dateTime >= currentWeekStart && dateTime < nextWeekStart;
            return dateTimeIsOnCurrentWeek;
        }
    }
}