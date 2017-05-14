using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MojDziennikv4.Models;
using MojDziennikv4.Models.DAL;

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
            events.Clear();
            Pupil pupil = PersonAccount.GetPupilFromAccountId(person.accountId);
            foreach (var eve in db.Event.ToList())
            {
                ap.accountGroup = eve.Account_Group;
                ap.init();

                if (ap.exits(person.AuthenticationType, pupil.Class_Number))
                {
                    events.Add(eve);
                }
            }
            return PartialView(events);
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
                if (ap.exits(person.AuthenticationType, classNumer))
                {
                    events.Add(eve);
                }
            }
            return PartialView("TeacherEvents",events);
        }
    }
}