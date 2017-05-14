using MojDziennikv4.Filters;
using MojDziennikv4.Models;
using MojDziennikv4.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MojDziennikv4.Controllers
{
    [BasicAuthentication]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.userName = "Administrator";
            ViewBag.hlp = null;
            return View();
        }

        public ActionResult AsPupil()
        {
            ViewBag.Message = "to jest panel ucznia";
            
            return View((Pupil)GetPerson());
        }

        public ActionResult AsTeacher()
        {
            ViewBag.Message = "to jest panel nauczyciela";

            return View();
        }
        public ActionResult LoginName()
        {
            return PartialView("_LoginPartial", GetPerson());
        }
        public static IPerson GetPerson()
        {
            MojDziennikEntities db = new MojDziennikEntities();
            switch (PersonAccount.getInstance().AuthenticationType)
            {
                case "Nauczyciel": return GetEmployeeFromAccountId(db, PersonAccount.getInstance().accountId);
                case "Uczen": return GetPupilFromAccountId(db, PersonAccount.getInstance().accountId);
                case "Opiekun": return GetLegal_GuardianFromAccountId(db, PersonAccount.getInstance().accountId);
                default: return GetEmployeeFromAccountId(db, PersonAccount.getInstance().accountId);
            }
        }
        public static Employee GetEmployeeFromAccountId(MojDziennikEntities db,int accountid)
        {
            var account = db.Account.ToList().Where(a => a.Account_Id == accountid);
            var temp = account.FirstOrDefault().Employee;
            return temp.ElementAt(0);
        }
        public static Pupil GetPupilFromAccountId(MojDziennikEntities db,int accountid)
        {
            var account = db.Account.ToList().Where(a => a.Account_Id == accountid);
            var temp = account.FirstOrDefault().Pupil;
            return temp.ElementAt(0);
        }
        public static Legal_Guardian GetLegal_GuardianFromAccountId(MojDziennikEntities db,int accountid)
        {
            var account = db.Account.ToList().Where(a => a.Account_Id == accountid);
            var temp = account.FirstOrDefault().Legal_Guardian;
            return temp.ElementAt(0);
        }
    }
}