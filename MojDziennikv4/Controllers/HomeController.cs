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

        [AdminAuthorization]
        public ActionResult Index()
        {
            ViewBag.userName = "Administrator";
            ViewBag.hlp = null;
            return View(LogManager.GetAllLogs());
        }
        [PupilAuthorization]
        public ActionResult AsPupil()
        {
            Pupil person = (Pupil)Person.GetPerson();
            ViewBag.PupilDetails = new Pair<string, string>[4]
            {
             new Pair<string, string>("Imie i Nazwisko",person.First_Name+" "+person.Surname),
             new Pair<string, string>("Data urodzenia ", person.Birth_Date.ToString()),
            new Pair<string, string>("Numer legitymacji ", person.School_Id_Card_Number),
             new Pair<string, string>("Klasa ", person.Class_Number)
            };
            ViewBag.Message = "to jest panel ucznia";

            return View(person);
        }
        [EmployeeAuthorization]
        public ActionResult AsTeacher()
        {
            ViewBag.Message = "to jest panel nauczyciela";

            return View();
        }
        public ActionResult LoginName()
        {
            using (MojDziennikEntities db = new MojDziennikEntities())
            {
                PersonAccount person = PersonAccount.getInstance();
                if (person.legalGuardianLog)
                    return PartialView("_LoginPartial", db.Legal_Guardian.Where(a => a.Pupil.ToList().FirstOrDefault().Account_Id == person.accountId).ToList().FirstOrDefault());
                return PartialView("_LoginPartial", Person.GetPerson());
            }
        }

    }
}