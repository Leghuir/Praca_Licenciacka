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
    public class EventsController : Controller
    {
        private MojDziennikEntities db = new MojDziennikEntities();

        // GET: Events
        public ActionResult Index([Form] QueryOptions<String> queryOptions)
        {
            var temp1 = db.Event.ToList();
            double temp = temp1.Count / (double)queryOptions.pageSize;
            queryOptions.totalPage = (int)Math.Ceiling(temp);
            var start = (queryOptions.currnetPage - 1) * queryOptions.pageSize;
            ViewBag.QueryOptions = queryOptions;
            if (queryOptions.Searchitem != "" && queryOptions.Searchitem != null)
                return View(db.Event.Where(a => a.Employee.Surname.IndexOf(queryOptions.Searchitem) != -1 ||
                a.Account_Group.IndexOf(queryOptions.Searchitem) != -1 ||
                a.Describe.IndexOf(queryOptions.Searchitem) != -1 ||
                a.Duration_In_Days.Value.ToString().IndexOf(queryOptions.Searchitem) != -1 ||
                a.Start_Time.Value.ToString().IndexOf(queryOptions.Searchitem) != -1
                ).ToList()); //leter i could connect them
            return View(db.Event.OrderBy(queryOptions.Sort).Skip(start).Take(queryOptions.pageSize).ToList());

        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Event.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            ViewBag.Employee_Id = new SelectList(db.Employee, "Employee_Id", "First_Name");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Event_Id,Employee_Id,Start_Time,Duration_In_Days,Describe,Account_Group")] Event @event)
        {
            LogManager.createlog("create", @event.ToString());
            if (ModelState.IsValid)
            {
                db.Event.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Employee_Id = new SelectList(db.Employee, "Employee_Id", "First_Name", @event.Employee_Id);
            return View(@event);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Event.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.Employee_Id = new SelectList(db.Employee, "Employee_Id", "First_Name", @event.Employee_Id);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Event_Id,Employee_Id,Start_Time,Duration_In_Days,Describe,Account_Group")] Event @event)
        {
            String accountTemp = "";
            using (MojDziennikEntities tempdb = new MojDziennikEntities())
            {
                accountTemp = tempdb.Event.Find(@event.Event_Id).ToString();
            }
            LogManager.createlog("Edit", accountTemp.ToString());
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Employee_Id = new SelectList(db.Employee, "Employee_Id", "First_Name", @event.Employee_Id);
            return View(@event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Event.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Event.Find(id);
            db.Event.Remove(@event);
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
