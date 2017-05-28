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
    public class NotesController : Controller
    {
        private MojDziennikEntities db = new MojDziennikEntities();

        // GET: Notes
        public ActionResult Index([Form] QueryOptions<String> queryOptions)
        {
            ViewBag.QueryOptions = queryOptions;
            if (queryOptions.Searchitem != "" && queryOptions.Searchitem != null)
                return View(db.Note.Where(a => a.Employee.Surname.IndexOf(queryOptions.Searchitem) != -1 ||
                a.Describe.IndexOf(queryOptions.Searchitem) != -1 ||
                a.Note_Date.ToString().IndexOf(queryOptions.Searchitem) != -1 ||
                a.Positve.ToString().IndexOf(queryOptions.Searchitem) != -1 ||
                a.Pupil.Surname.IndexOf(queryOptions.Searchitem) != -1
                ).ToList()); //leter i could connect them
            var note = db.Note.Include(n => n.Employee).Include(n => n.Pupil);
            return View(db.Note.OrderBy(queryOptions.Sort).ToList());
        }

        // GET: Notes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = db.Note.Find(id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        // GET: Notes/Create
        public ActionResult Create()
        {
            ViewBag.Employee_Id = new SelectList(db.Employee, "Employee_ID", "First_Name");
            ViewBag.Pupil_Id = new SelectList(db.Pupil, "Pupil_Id", "First_Name");
            return View();
        }

        // POST: Notes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Note_Id,Pupil_Id,Employee_Id,Note_Date,Positve,Describe")] Note note)
        {
            LogManager.createlog("create", note.ToString());
            if (ModelState.IsValid)
            {
                db.Note.Add(note);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Employee_Id = new SelectList(db.Employee, "Employee_ID", "First_Name", note.Employee_Id);
            ViewBag.Pupil_Id = new SelectList(db.Pupil, "Pupil_Id", "First_Name", note.Pupil_Id);
            return View(note);
        }

        // GET: Notes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = db.Note.Find(id);
            if (note == null)
            {
                return HttpNotFound();
            }
            ViewBag.Employee_Id = new SelectList(db.Employee, "Employee_ID", "First_Name", note.Employee_Id);
            ViewBag.Pupil_Id = new SelectList(db.Pupil, "Pupil_Id", "First_Name", note.Pupil_Id);
            return View(note);
        }

        // POST: Notes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Note_Id,Pupil_Id,Employee_Id,Note_Date,Positve,Describe")] Note note)
        {
            Note accountTemp = db.Note.Find(note.Note_Id);
            LogManager.createlog("Edit", accountTemp.ToString());
            if (ModelState.IsValid)
            {
                db.Entry(note).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Employee_Id = new SelectList(db.Employee, "Employee_ID", "First_Name", note.Employee_Id);
            ViewBag.Pupil_Id = new SelectList(db.Pupil, "Pupil_Id", "First_Name", note.Pupil_Id);
            return View(note);
        }

        // GET: Notes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = db.Note.Find(id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Note account = db.Note.Find(id);
            LogManager.createlog("delete", account.ToString());
            Note note = db.Note.Find(id);
            db.Note.Remove(note);
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
