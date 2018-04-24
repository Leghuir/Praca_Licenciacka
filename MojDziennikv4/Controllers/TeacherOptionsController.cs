using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MojDziennikv4.Models;
using MojDziennikv4.Filters;
using MojDziennikv4.Models.DAL;

namespace MojDziennikv4.Controllers
{
    [EmployeeAuthorization]
    public class TeacherOptionsController : Controller
    {
        private static String sub;
        private MojDziennikEntities db = new MojDziennikEntities();

        // GET: TeacherOptions
        [HttpPost]
        public ActionResult AddMarks(int[] Id, String[] Value, String[] Weight, String[] Description, String Subject1)
        {
            if (Id == null && Subject1 == null)
            {
                sub = null;
            }
            if (Subject1 != null)
            {
                sub = Subject1;
            }
            Console.WriteLine();
            if (Id == null)
                Id = new int[0];
            for (int i = 0; i < Id.Length; i++)
            {
                if (sub != null && Value[i]!="" && Weight[i]!="")
                {
                    Mark mark = new Mark();
                    mark.Describe = Description[i];
                    mark.Value = int.Parse(Value[i]);
                    mark.Weight = decimal.Parse(Weight[i].Replace('.', ','));
                    mark.Pupil_Id = Id[i];
                    mark.Mark_Date = DateTime.Now;
                    mark.Employee_Id = PersonAccount.GetEmployeeFromAccountId().Employee_Id;
                    mark.Subject_Id = db.Subject.Where(a => a.Subject_Name.Equals(sub)).ToList().ElementAt(0).Subject_Id;
                    db.Mark.Add(mark);
                    db.SaveChanges();
                    LogManager.createlog("create", mark.ToString());
                }

            }
            ViewBag.Class_Name = db.School_Class.ToList();
            ViewBag.Subjects = db.Subject.Select(a => a.Subject_Name).ToList();
            return View();
        }
        public ActionResult AddMarks()
        {
            ViewBag.Class_Name = db.School_Class.ToList();
            ViewBag.Subjects = db.Subject.Select(a => a.Subject_Name).ToList();
            ViewBag.Employee_Id = new SelectList(db.Employee, "Employee_ID", "First_Name");
            ViewBag.Subject_Id = new SelectList(db.Subject, "Subject_Id", "Subject_Name");
            ViewBag.Pupil_Id = new SelectList(db.Pupil, "Pupil_Id", "First_Name");
            return View();
        }
        public ActionResult AddNotes()
        {
            ViewBag.Employee_Id = new SelectList(db.Employee, "Employee_ID", "First_Name");
            ViewBag.Pupil_Id = new SelectList(db.Pupil, "Pupil_Id", "Surname");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNotes([Bind(Include = "Note_Id,Pupil_Id,Employee_Id,Note_Date,Positve,Describe")] Note note)
        {
            note.Employee_Id = db.Account.Find(PersonAccount.getInstance().accountId).Employee.ElementAt(0).Employee_Id;
            if (note.Note_Date == null)
            {
                note.Note_Date = DateTime.Now;
            }
            LogManager.createlog("create", note.ToString());
            if (ModelState.IsValid)
            {
                db.Note.Add(note);
                db.SaveChanges();
                return Redirect("/Home/AsTeacher");
            }
            return View();
        }
        public ActionResult AddEvents()
        {
            ViewBag.Employee_Id = new SelectList(db.Employee, "Employee_Id", "First_Name");
            return View();
        }
        [HttpPost]
        public ActionResult AddEvents([Bind(Include = "Event_Id,Employee_Id,Start_Time,Duration_In_Days,Describe,Account_Group")] Event @event)
        {
            @event.Employee_Id = db.Account.Find(PersonAccount.getInstance().accountId).Employee.ElementAtOrDefault(0).Employee_Id;
            LogManager.createlog("create", @event.ToString());
            if (ModelState.IsValid)
            {
                db.Event.Add(@event);
                db.SaveChanges();
                return Redirect("/Home/AsTeacher");
            }

            ViewBag.Employee_Id = new SelectList(db.Employee, "Employee_Id", "First_Name", @event.Employee_Id);
            return View(@event);
        }
        public ActionResult AboutPupils()
        {
            ViewBag.Class_Name = db.School_Class.ToList();
            return View();
        }
        public ActionResult PartialAddMakrs(String Klasa)
        {
            return PartialView(db.Pupil.Where(a => a.Class_Number.Equals(Klasa)).ToList());
        }
        public ActionResult PupilMarks()
        {
            ViewBag.Class_Name = db.School_Class.ToList();
            ViewBag.Subjects = db.Subject.Select(a => a.Subject_Name).ToList();
            ViewBag.Employee_Id = new SelectList(db.Employee, "Employee_ID", "First_Name");
            ViewBag.Subject_Id = new SelectList(db.Subject, "Subject_Id", "Subject_Name");
            ViewBag.Pupil_Id = new SelectList(db.Pupil, "Pupil_Id", "First_Name");
            return View(); 
        }
        public ActionResult PartialPupilMarks(String SchoolClass,String Subject)
        {
            Dictionary<String, List<Mark>> pupilsMarks = new Dictionary<string, List<Mark>>();
            Pupil[] pupils = db.Pupil.Where(a => a.School_Class.Class_Number.Equals(SchoolClass)).ToArray();
            for(int i=0;i<pupils.Length;i++)
            { 
                    
                    pupilsMarks.Add(pupils[i].First_Name+" "+pupils[i].Surname, pupils[i].Mark.Where(a => a.Subject.Subject_Name.Equals(Subject)).ToList());
            }
            return PartialView(pupilsMarks);
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
