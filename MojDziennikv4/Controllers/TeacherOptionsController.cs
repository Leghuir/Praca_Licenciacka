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
    public class TeacherOptionsController : Controller
    {
        private MojDziennikEntities db = new MojDziennikEntities();

        // GET: TeacherOptions
        public ActionResult AddMarks()
        {
            ViewBag.Employee_Id = new SelectList(db.Employee, "Employee_ID", "First_Name");
            ViewBag.Subject_Id = new SelectList(db.Subject, "Subject_Id", "Subject_Name");
            ViewBag.Pupil_Id = new SelectList(db.Pupil, "Pupil_Id", "First_Name");
            return View();
        }
        public ActionResult AddNotes()
        {
            ViewBag.Employee_Id = new SelectList(db.Employee, "Employee_ID", "First_Name");
            ViewBag.Pupil_Id = new SelectList(db.Pupil, "Pupil_Id", "First_Name");
            return View();
        }
        public ActionResult AddEvents()
        {
            ViewBag.Employee_Id = new SelectList(db.Employee, "Employee_Id", "First_Name");
            return View();
        }
        public ActionResult AboutPupils()
        {
            ViewBag.Class_Name = db.School_Class.ToList();
            return View();
        }

    }
}
