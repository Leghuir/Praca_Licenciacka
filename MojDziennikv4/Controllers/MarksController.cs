using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using MojDziennikv4.Models;
using MojDziennikv4.Filters;
using System.Web.ModelBinding;

namespace MojDziennikv4.Controllers
{
    [AdminAuthorization]
    public class MarksController : Controller
    {
        private MojDziennikEntities db = new MojDziennikEntities();

        // GET: Marks
        public ActionResult Index([Form] QueryOptions<String> queryOptions)
        {
            ViewBag.QueryOptions = queryOptions;
            if (queryOptions.Searchitem != "" && queryOptions.Searchitem != null)
                return View(db.Mark.Where(a => a.Pupil.Surname.IndexOf(queryOptions.Searchitem) != -1 ||
                a.Describe.IndexOf(queryOptions.Searchitem) != -1 ||
                a.Employee.Surname.IndexOf(queryOptions.Searchitem) != -1 ||
                a.Mark_Date.ToString().IndexOf(queryOptions.Searchitem) != -1 ||
                a.Subject.Subject_Name.IndexOf(queryOptions.Searchitem) != -1 ||
                a.Value.ToString().IndexOf(queryOptions.Searchitem) != -1 ||
                a.Weight.Value.ToString().IndexOf(queryOptions.Searchitem) != -1 
                ).ToList()); //leter i could connect them
            return View(db.Mark.OrderBy(queryOptions.Sort).ToList());
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
            Mark accountTemp = db.Mark.Find(mark.Mark_Id);
            LogManager.createlog("Edit", accountTemp.ToString());
            LogManager.createlog("create", mark.ToString());
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
            Mark accountTemp = db.Mark.Find(mark.Mark_Id);
            LogManager.createlog("Edit", accountTemp.ToString());
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
            Mark account = db.Mark.Find(id);
            LogManager.createlog("delete", account.ToString());
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
