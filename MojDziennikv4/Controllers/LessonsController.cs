using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MojDziennikv4.Models;
using MojDziennikv4.Filters;
using System.Web.ModelBinding;

namespace MojDziennikv4.Controllers
{
    [AdminAuthorization]
    public class LessonsController : Controller
    {
        private MojDziennikEntities db = new MojDziennikEntities();

        // GET: Lessons
        public ActionResult Index([Form] QueryOptions<String> queryOptions)
        {
            ViewBag.QueryOptions = queryOptions;
            if (queryOptions.Searchitem != "" && queryOptions.Searchitem != null)
                return View(db.Lesson.Where(a => a.Class_Number.IndexOf(queryOptions.Searchitem) != -1 ||
                a.Class_Room_Number.IndexOf(queryOptions.Searchitem) != -1 ||
                a.Day_Of_Week.ToString().IndexOf(queryOptions.Searchitem) != -1 ||
                a.Start_Time.ToString().IndexOf(queryOptions.Searchitem) != -1 ||
                a.Subject.Subject_Name.IndexOf(queryOptions.Searchitem) != -1).ToList()); //leter i could connect them
            return View(db.Lesson.OrderBy(queryOptions.Sort).ToList());
        }

        // GET: Lessons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = db.Lesson.Find(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            return View(lesson);
        }

        // GET: Lessons/Create
        public ActionResult Create()
        {
            ViewBag.Class_Room_Number = new SelectList(db.Class_Room, "Class_Room_Number", "Class_Room_Number");
            ViewBag.Employee_Id = new SelectList(db.Employee, "Employee_ID", "First_Name");
            ViewBag.Class_Number = new SelectList(db.School_Class, "Class_Number", "Profile");
            ViewBag.Subject_Id = new SelectList(db.Subject, "Subject_Id", "Subject_Name");
            return View();
        }

        // POST: Lessons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Lesson_Id,Class_Number,Subject_Id,Employee_Id,Day_Of_Week,Start_Time,Class_Room_Number")] Lesson lesson)
        {
            LogManager.createlog("create", lesson.ToString());
            if (ModelState.IsValid)
            {
                db.Lesson.Add(lesson);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Class_Room_Number = new SelectList(db.Class_Room, "Class_Room_Number", "Class_Room_Number", lesson.Class_Room_Number);
            ViewBag.Employee_Id = new SelectList(db.Employee, "Employee_ID", "First_Name", lesson.Employee_Id);
            ViewBag.Class_Number = new SelectList(db.School_Class, "Class_Number", "Profile", lesson.Class_Number);
            ViewBag.Subject_Id = new SelectList(db.Subject, "Subject_Id", "Subject_Name", lesson.Subject_Id);
            return View(lesson);
        }

        // GET: Lessons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = db.Lesson.Find(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            ViewBag.Class_Room_Number = new SelectList(db.Class_Room, "Class_Room_Number", "Class_Room_Number", lesson.Class_Room_Number);
            ViewBag.Employee_Id = new SelectList(db.Employee, "Employee_ID", "First_Name", lesson.Employee_Id);
            ViewBag.Class_Number = new SelectList(db.School_Class, "Class_Number", "Profile", lesson.Class_Number);
            ViewBag.Subject_Id = new SelectList(db.Subject, "Subject_Id", "Subject_Name", lesson.Subject_Id);
            return View(lesson);
        }

        // POST: Lessons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Lesson_Id,Class_Number,Subject_Id,Employee_Id,Day_Of_Week,Start_Time,Class_Room_Number")] Lesson lesson)
        {
            String accountTemp = "";
            using (MojDziennikEntities tempdb = new MojDziennikEntities())
            {
                accountTemp = tempdb.Lesson.Find(lesson.Lesson_Id).ToString();
            }
            LogManager.createlog("Edit", accountTemp);
            if (ModelState.IsValid)
            {
                db.Entry(lesson).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Class_Room_Number = new SelectList(db.Class_Room, "Class_Room_Number", "Class_Room_Number", lesson.Class_Room_Number);
            ViewBag.Employee_Id = new SelectList(db.Employee, "Employee_ID", "First_Name", lesson.Employee_Id);
            ViewBag.Class_Number = new SelectList(db.School_Class, "Class_Number", "Profile", lesson.Class_Number);
            ViewBag.Subject_Id = new SelectList(db.Subject, "Subject_Id", "Subject_Name", lesson.Subject_Id);
            return View(lesson);
        }

        // GET: Lessons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = db.Lesson.Find(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            return View(lesson);
        }

        // POST: Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lesson account = db.Lesson.Find(id);
            LogManager.createlog("delete", account.ToString());
            Lesson lesson = db.Lesson.Find(id);
            db.Lesson.Remove(lesson);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
