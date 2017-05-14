using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MojDziennikv4.Models;

namespace MojDziennikv4.Controllers
{
    public class School_ClassController : Controller
    {
        private MojDziennikEntities db = new MojDziennikEntities();

        // GET: School_Class
        public ActionResult Index()
        {
            var school_Class = db.School_Class.Include(s => s.Employee).Include(s => s.Lesson);
            return View(school_Class.ToList());
        }

        // GET: School_Class/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School_Class school_Class = db.School_Class.Find(id);
            if (school_Class == null)
            {
                return HttpNotFound();
            }
            return View(school_Class);
        }

        // GET: School_Class/Create
        public ActionResult Create()
        {
            ViewBag.Employee_Id = new SelectList(db.Employee, "Employee_ID", "First_Name");
            ViewBag.Class_Number = new SelectList(db.Lesson, "Class_Number", "Subject_Name");
            return View();
        }

        // POST: School_Class/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Class_Number,Profile,Employee_Id")] School_Class school_Class)
        {
            if (ModelState.IsValid)
            {
                db.School_Class.Add(school_Class);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Employee_Id = new SelectList(db.Employee, "Employee_ID", "First_Name", school_Class.Employee_Id);
            ViewBag.Class_Number = new SelectList(db.Lesson, "Class_Number", "Subject_Name", school_Class.Class_Number);
            return View(school_Class);
        }

        // GET: School_Class/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School_Class school_Class = db.School_Class.Find(id);
            if (school_Class == null)
            {
                return HttpNotFound();
            }
            ViewBag.Employee_Id = new SelectList(db.Employee, "Employee_ID", "First_Name", school_Class.Employee_Id);
            ViewBag.Class_Number = new SelectList(db.Lesson, "Class_Number", "Subject_Name", school_Class.Class_Number);
            return View(school_Class);
        }

        // POST: School_Class/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Class_Number,Profile,Employee_Id")] School_Class school_Class)
        {
            if (ModelState.IsValid)
            {
                db.Entry(school_Class).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Employee_Id = new SelectList(db.Employee, "Employee_ID", "First_Name", school_Class.Employee_Id);
            ViewBag.Class_Number = new SelectList(db.Lesson, "Class_Number", "Subject_Name", school_Class.Class_Number);
            return View(school_Class);
        }

        // GET: School_Class/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School_Class school_Class = db.School_Class.Find(id);
            if (school_Class == null)
            {
                return HttpNotFound();
            }
            return View(school_Class);
        }

        // POST: School_Class/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            School_Class school_Class = db.School_Class.Find(id);
            db.School_Class.Remove(school_Class);
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
