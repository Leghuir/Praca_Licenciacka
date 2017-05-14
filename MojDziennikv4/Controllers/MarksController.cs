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
    public class MarksController : Controller
    {
        private MojDziennikEntities db = new MojDziennikEntities();

        // GET: Marks
        public ActionResult Index()
        {
            var mark = db.Mark.Include(m => m.Employee).Include(m => m.Subject).Include(m => m.Pupil);
            return View(mark.ToList());
        }

        // GET: Marks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mark mark = db.Mark.Find(id);
            if (mark == null)
            {
                return HttpNotFound();
            }
            return View(mark);
        }

        // GET: Marks/Create
        public ActionResult Create()
        {
            ViewBag.Employee_Id = new SelectList(db.Employee, "Employee_ID", "First_Name");
            ViewBag.Subject_Id = new SelectList(db.Subject, "Subject_Id", "Subject_Name");
            ViewBag.Pupil_Id = new SelectList(db.Pupil, "Pupil_Id", "First_Name");
            return View();
        }

        // POST: Marks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Mark_Id,Value,Weight,Pupil_Id,Employee_Id,Subject_Id,Describe,Mark_Date")] Mark mark)
        {
            if (ModelState.IsValid)
            {
                db.Mark.Add(mark);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Employee_Id = new SelectList(db.Employee, "Employee_ID", "First_Name", mark.Employee_Id);
            ViewBag.Subject_Id = new SelectList(db.Subject, "Subject_Id", "Subject_Name", mark.Subject_Id);
            ViewBag.Pupil_Id = new SelectList(db.Pupil, "Pupil_Id", "First_Name", mark.Pupil_Id);
            return View(mark);
        }

        // GET: Marks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mark mark = db.Mark.Find(id);
            if (mark == null)
            {
                return HttpNotFound();
            }
            ViewBag.Employee_Id = new SelectList(db.Employee, "Employee_ID", "First_Name", mark.Employee_Id);
            ViewBag.Subject_Id = new SelectList(db.Subject, "Subject_Id", "Subject_Name", mark.Subject_Id);
            ViewBag.Pupil_Id = new SelectList(db.Pupil, "Pupil_Id", "First_Name", mark.Pupil_Id);
            return View(mark);
        }

        // POST: Marks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Mark_Id,Value,Weight,Pupil_Id,Employee_Id,Subject_Id,Describe,Mark_Date")] Mark mark)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mark).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Employee_Id = new SelectList(db.Employee, "Employee_ID", "First_Name", mark.Employee_Id);
            ViewBag.Subject_Id = new SelectList(db.Subject, "Subject_Id", "Subject_Name", mark.Subject_Id);
            ViewBag.Pupil_Id = new SelectList(db.Pupil, "Pupil_Id", "First_Name", mark.Pupil_Id);
            return View(mark);
        }

        // GET: Marks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mark mark = db.Mark.Find(id);
            if (mark == null)
            {
                return HttpNotFound();
            }
            return View(mark);
        }

        // POST: Marks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mark mark = db.Mark.Find(id);
            db.Mark.Remove(mark);
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
