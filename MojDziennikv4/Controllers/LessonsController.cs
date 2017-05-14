﻿using System;
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
    public class LessonsController : Controller
    {
        private MojDziennikEntities db = new MojDziennikEntities();

        // GET: Lessons
        public ActionResult Index()
        {
            var lesson = db.Lesson.Include(l => l.Class_Room).Include(l => l.Employee).Include(l => l.School_Class).Include(l => l.Subject);
            return View(lesson.ToList());
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
