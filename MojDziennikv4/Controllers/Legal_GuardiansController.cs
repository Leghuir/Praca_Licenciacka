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
    public class Legal_GuardiansController : Controller
    {
        private MojDziennikEntities db = new MojDziennikEntities();

        // GET: Legal_Guardians
        public ActionResult Index()
        {
            var legal_Guardian = db.Legal_Guardian.Include(l => l.Account);
            return View(legal_Guardian.ToList());
        }

        // GET: Legal_Guardians/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Legal_Guardian legal_Guardian = db.Legal_Guardian.Find(id);
            if (legal_Guardian == null)
            {
                return HttpNotFound();
            }
            return View(legal_Guardian);
        }

        // GET: Legal_Guardians/Create
        public ActionResult Create()
        {
            ViewBag.Account_Id = new SelectList(db.Account, "Account_Id", "Login");
            return View();
        }

        // POST: Legal_Guardians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Legal_Guardian_Id,First_Name,Middle_Name,Surname,Telefon_Number,Account_Id,Adress")] Legal_Guardian legal_Guardian)
        {
            if (ModelState.IsValid)
            {
                db.Legal_Guardian.Add(legal_Guardian);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Account_Id = new SelectList(db.Account, "Account_Id", "Login", legal_Guardian.Account_Id);
            return View(legal_Guardian);
        }

        // GET: Legal_Guardians/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Legal_Guardian legal_Guardian = db.Legal_Guardian.Find(id);
            if (legal_Guardian == null)
            {
                return HttpNotFound();
            }
            ViewBag.Account_Id = new SelectList(db.Account, "Account_Id", "Login", legal_Guardian.Account_Id);
            return View(legal_Guardian);
        }

        // POST: Legal_Guardians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Legal_Guardian_Id,First_Name,Middle_Name,Surname,Telefon_Number,Account_Id,Adress")] Legal_Guardian legal_Guardian)
        {
            if (ModelState.IsValid)
            {
                db.Entry(legal_Guardian).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Account_Id = new SelectList(db.Account, "Account_Id", "Login", legal_Guardian.Account_Id);
            return View(legal_Guardian);
        }

        // GET: Legal_Guardians/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Legal_Guardian legal_Guardian = db.Legal_Guardian.Find(id);
            if (legal_Guardian == null)
            {
                return HttpNotFound();
            }
            return View(legal_Guardian);
        }

        // POST: Legal_Guardians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Legal_Guardian legal_Guardian = db.Legal_Guardian.Find(id);
            db.Legal_Guardian.Remove(legal_Guardian);
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
