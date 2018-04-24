using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MojDziennikv4.Models;
using MojDziennikv4.Models.DAL;
using System.Globalization;
using System.Threading;
namespace MojDziennikv4.Controllers
{

    public class ParticalEventsController : Controller
    {
        private MojDziennikEntities db = new MojDziennikEntities();
        AccountGroupParserController ap = new AccountGroupParserController();
        List<Event> events = new List<Event>();
        PersonAccount person = PersonAccount.getInstance();
        // GET: ParticalEvents
        public ActionResult Index()
        {
            Pupil pupile = PersonAccount.GetPupilFromAccountId();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pl-PL");
            String type = PersonAccount.getInstance().AuthenticationType;
            return PartialView(db.Event.ToList().Where(a => AccountGroupParserController.EventMatcher(a, type, pupile.Class_Number)).ToList());
        }
        public ActionResult TeacherEvents()
        {
            events.Clear();
            Employee teacher = PersonAccount.GetEmployeeFromAccountId();
            foreach (var eve in db.Event.ToList())
            {
                ap.accountGroup = eve.Account_Group;
                ap.init();
                var temp=teacher.School_Class.ToList().ElementAtOrDefault(0);
                String classNumer = "";
                if (temp != null)
                    classNumer = temp.Class_Number;
                if (ap.exists(person.AuthenticationType, classNumer))
                {
                    events.Add(eve);
                }
            }
            return PartialView("TeacherEvents",events);
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