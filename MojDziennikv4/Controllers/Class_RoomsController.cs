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
    public class Class_RoomsController : Controller
    {
        private MojDziennikEntities db = new MojDziennikEntities();

        // GET: Class_Rooms
        public ActionResult Index()
        {
            var class_Room = db.Class_Room.Include(c => c.Employee);
            return View(class_Room.ToList());
        }

        // GET: Class_Rooms/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class_Room class_Room = db.Class_Room.Find(id);
            if (class_Room == null)
            {
                return HttpNotFound();
            }
            return View(class_Room);
        }

        // GET: Class_Rooms/Create
        public ActionResult Create()
        {
            ViewBag.Employee_Id = new SelectList(db.Employee, "Employee_ID", "First_Name");
            return View();
        }

        // POST: Class_Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Class_Room_Number,Employee_Id,Size,Subject_Id,Equipment_Description")] Class_Room class_Room)
        {
            if (ModelState.IsValid)
            {
                db.Class_Room.Add(class_Room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Employee_Id = new SelectList(db.Employee, "Employee_ID", "First_Name", class_Room.Employee_Id);
            return View(class_Room);
        }

        // GET: Class_Rooms/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class_Room class_Room = db.Class_Room.Find(id);
            if (class_Room == null)
            {
                return HttpNotFound();
            }
            ViewBag.Employee_Id = new SelectList(db.Employee, "Employee_ID", "First_Name", class_Room.Employee_Id);
            return View(class_Room);
        }

        // POST: Class_Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Class_Room_Number,Employee_Id,Size,Subject_Id,Equipment_Description")] Class_Room class_Room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(class_Room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Employee_Id = new SelectList(db.Employee, "Employee_ID", "First_Name", class_Room.Employee_Id);
            return View(class_Room);
        }

        // GET: Class_Rooms/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class_Room class_Room = db.Class_Room.Find(id);
            if (class_Room == null)
            {
                return HttpNotFound();
            }
            return View(class_Room);
        }

        // POST: Class_Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Class_Room class_Room = db.Class_Room.Find(id);
            db.Class_Room.Remove(class_Room);
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
