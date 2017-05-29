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
    public class PupilsController : Controller
    {
        private MojDziennikEntities db = new MojDziennikEntities();

        // GET: Pupils
        public ActionResult Index([Form] QueryOptions<String> queryOptions)
        {
            ViewBag.QueryOptions = queryOptions;
            if (queryOptions.Searchitem != "" && queryOptions.Searchitem != null)
                return View(db.Pupil.Where(a => a.Surname.IndexOf(queryOptions.Searchitem) != -1 ||
                a.Account.Login.IndexOf(queryOptions.Searchitem) != -1 ||
                a.Class_Number.IndexOf(queryOptions.Searchitem) != -1 ||
                a.First_Name.IndexOf(queryOptions.Searchitem) != -1 ||
                a.Legal_Guardian.First_Name.IndexOf(queryOptions.Searchitem) != -1 ||
                a.Middle_Name.IndexOf(queryOptions.Searchitem) != -1 ||
                a.Pesel_Number.IndexOf(queryOptions.Searchitem) != -1 ||
                a.School_Id_Card_Number.IndexOf(queryOptions.Searchitem) != -1 ||
                a.Birth_Date.ToString().IndexOf(queryOptions.Searchitem) != -1 


                ).ToList()); //leter i could connect them
            return View(db.Pupil.OrderBy(queryOptions.Sort).ToList());
        }

        // GET: Pupils/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pupil pupil = db.Pupil.Find(id);
            if (pupil == null)
            {
                return HttpNotFound();
            }
            return View(pupil);
        }

        // GET: Pupils/Create
        public ActionResult Create()
        {
            ViewBag.Account_Id = new SelectList(db.Account, "Account_Id", "Login");
            ViewBag.Legal_Guardian_Id = new SelectList(db.Legal_Guardian, "Legal_Guardian_Id", "First_Name");
            ViewBag.Class_Number = new SelectList(db.School_Class, "Class_Number", "Profile");
            return View();
        }

        // POST: Pupils/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pupil_Id,First_Name,Middle_Name,Surname,Pesel_Number,Legal_Guardian_Id,Birth_Date,School_Id_Card_Number,Account_Id,Class_Number")] Pupil pupil)
        {
            LogManager.createlog("create", pupil.ToString());
            if (ModelState.IsValid)
            {
                db.Pupil.Add(pupil);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Account_Id = new SelectList(db.Account, "Account_Id", "Login", pupil.Account_Id);
            ViewBag.Legal_Guardian_Id = new SelectList(db.Legal_Guardian, "Legal_Guardian_Id", "First_Name", pupil.Legal_Guardian_Id);
            ViewBag.Class_Number = new SelectList(db.School_Class, "Class_Number", "Profile", pupil.Class_Number);
            return View(pupil);
        }

        // GET: Pupils/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pupil pupil = db.Pupil.Find(id);
            if (pupil == null)
            {
                return HttpNotFound();
            }
            ViewBag.Account_Id = new SelectList(db.Account, "Account_Id", "Login", pupil.Account_Id);
            ViewBag.Legal_Guardian_Id = new SelectList(db.Legal_Guardian, "Legal_Guardian_Id", "First_Name", pupil.Legal_Guardian_Id);
            ViewBag.Class_Number = new SelectList(db.School_Class, "Class_Number", "Profile", pupil.Class_Number);
            return View(pupil);
        }

        // POST: Pupils/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pupil_Id,First_Name,Middle_Name,Surname,Pesel_Number,Legal_Guardian_Id,Birth_Date,School_Id_Card_Number,Account_Id,Class_Number")] Pupil pupil)
        {
            String accountTemp = "";
            using (MojDziennikEntities tempdb = new MojDziennikEntities())
            {
                accountTemp = tempdb.Pupil.Find(pupil.Pupil_Id).ToString();
            }
            LogManager.createlog("Edit", accountTemp);
            if (ModelState.IsValid)
            {
                db.Entry(pupil).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Account_Id = new SelectList(db.Account, "Account_Id", "Login", pupil.Account_Id);
            ViewBag.Legal_Guardian_Id = new SelectList(db.Legal_Guardian, "Legal_Guardian_Id", "First_Name", pupil.Legal_Guardian_Id);
            ViewBag.Class_Number = new SelectList(db.School_Class, "Class_Number", "Profile", pupil.Class_Number);
            return View(pupil);
        }

        // GET: Pupils/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pupil pupil = db.Pupil.Find(id);
            if (pupil == null)
            {
                return HttpNotFound();
            }
            return View(pupil);
        }

        // POST: Pupils/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pupil account = db.Pupil.Find(id);
            LogManager.createlog("delete", account.ToString());
            Pupil pupil = db.Pupil.Find(id);
            db.Pupil.Remove(pupil);
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
