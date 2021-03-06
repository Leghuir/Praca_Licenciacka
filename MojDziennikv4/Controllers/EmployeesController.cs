﻿using System;
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
    public class EmployeesController : Controller
    {
        private MojDziennikEntities db = new MojDziennikEntities();

        // GET: Employees
        public ActionResult Index([Form] QueryOptions<String> queryOptions)
        {
            var temp1 = db.Employee.ToList();
            double temp = temp1.Count / (double)queryOptions.pageSize;
            queryOptions.totalPage = (int)Math.Ceiling(temp);
            var start = (queryOptions.currnetPage - 1) * queryOptions.pageSize;
            ViewBag.QueryOptions = queryOptions;
            if (queryOptions.Searchitem != "" && queryOptions.Searchitem != null)
                return View(db.Employee.Where(a => a.Surname.IndexOf(queryOptions.Searchitem) != -1 ||
                a.Account.Login.IndexOf(queryOptions.Searchitem) != -1 ||
                a.Adress.IndexOf(queryOptions.Searchitem) != -1 ||
                a.First_Name.IndexOf(queryOptions.Searchitem) != -1 ||
                a.Middle_Name.IndexOf(queryOptions.Searchitem) != -1 ||
                a.Pesel_Number.IndexOf(queryOptions.Searchitem) != -1 ||
                a.Telefon_Number.IndexOf(queryOptions.Searchitem) != -1 ||
                a.Birth_Date.Value.ToString().IndexOf(queryOptions.Searchitem) != -1 ||
                a.Hire_Date.Value.ToString().IndexOf(queryOptions.Searchitem) !=-1

                ).ToList()); //leter i could connect them
            return View(db.Employee.OrderBy(queryOptions.Sort).Skip(start).Take(queryOptions.pageSize).ToList());
            
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.Account_Id = new SelectList(db.Account, "Account_Id", "Login");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Employee_ID,First_Name,Middle_Name,Surname,Pesel_Number,Telefon_Number,Account_Id,Hire_Date,Birth_Date,Adress")] Employee employee)
        {
            LogManager.createlog("create", employee.ToString());
            if (ModelState.IsValid)
            {
                db.Employee.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Account_Id = new SelectList(db.Account, "Account_Id", "Login", employee.Account_Id);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.Account_Id = new SelectList(db.Account, "Account_Id", "Login", employee.Account_Id);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Employee_ID,First_Name,Middle_Name,Surname,Pesel_Number,Telefon_Number,Account_Id,Hire_Date,Birth_Date,Adress")] Employee employee)
        {
            String accountTemp = "";
            using (MojDziennikEntities tempdb = new MojDziennikEntities())
            {
                accountTemp = tempdb.Employee.Find(employee.Employee_Id).ToString();
            }
            LogManager.createlog("Edit", accountTemp);
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Account_Id = new SelectList(db.Account, "Account_Id", "Login", employee.Account_Id);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee account = db.Employee.Find(id);
            LogManager.createlog("delete", account.ToString());
            Employee employee = db.Employee.Find(id);
            db.Employee.Remove(employee);
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
