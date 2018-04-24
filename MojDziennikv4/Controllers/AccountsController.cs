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
using MojDziennikv4.Models.DAL;
using System.Web.ModelBinding;
using MojDziennikv4.Filters;

namespace MojDziennikv4.Controllers
{
    [AdminAuthorization]
    public class AccountsController : Controller
    {
        private MojDziennikEntities db = new MojDziennikEntities();

        // GET: Accounts

        public ActionResult Index([Form] QueryOptions<String> queryOptions)
        {
            var temp1 = db.Account.ToList();
            double temp = temp1.Count / (double)queryOptions.pageSize;
            queryOptions.totalPage = (int)Math.Ceiling(temp);

            var start = (queryOptions.currnetPage - 1) * queryOptions.pageSize;
            ViewBag.QueryOptions = queryOptions;
                if (queryOptions.Searchitem != "" && queryOptions.Searchitem != null)
                    return View(db.Account.Where(a => a.Login.IndexOf(queryOptions.Searchitem) != -1 ||
                     a.Account_Type.IndexOf(queryOptions.Searchitem) != -1 ||
                     a.Password.IndexOf(queryOptions.Searchitem) != -1
                    ).ToList()); //leter i could connect them
                return View(db.Account.OrderBy(queryOptions.Sort).Skip(start).Take(queryOptions.pageSize).ToList());
        }
        // GET: Accounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Account_Id,Login,Password,Account_Type")] Account account)
        {
            if (ModelState.IsValid)
            {
                LogManager.createlog("create", account.ToString());
                db.Account.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(account);
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Account.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Account_Id,Login,Password,Account_Type")] Account account)
        {
            String accountTemp = "";
            using (MojDziennikEntities tempdb = new MojDziennikEntities())
            {
                accountTemp = tempdb.Account.Find(account.Account_Id).ToString();
            }
            if (ModelState.IsValid)
            {
                LogManager.createlog("Edit", accountTemp);
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: Accounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Account.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account;
            using (var mydb = new MojDziennikEntities())
            {
                account = mydb.Account.Find(id);
                LogManager.createlog("delete", account.ToString());
            }
            account = db.Account.Find(id);
            db.Account.Remove(account);
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
