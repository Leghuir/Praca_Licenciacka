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
    [PupilAuthorization]
    public class PupilOptionsController : Controller
    {
        private MojDziennikEntities db = new MojDziennikEntities();
        private Pupil pupile;
        public PupilOptionsController()
        {
            pupile = PersonAccount.GetPupilFromAccountId();
        }
        // GET: PupilOptions
        public ActionResult LessonPlan()
        {
            return View(pupile);
        }
        public ActionResult Marks()
        {
            Dictionary<String, List<Mark>> puilsMarks = new Dictionary<string, List<Mark>>();
            foreach (var sub in pupile.School_Class.Lesson)
            {
                if (!puilsMarks.ContainsKey(sub.Subject.Subject_Name))
                    puilsMarks.Add(sub.Subject.Subject_Name, pupile.Mark.Where(a => a.Subject.Subject_Name.Equals(sub.Subject.Subject_Name)).ToList());
            }
                return View(puilsMarks);
            }
        public ActionResult Notes()
        {
            return View(pupile.Note.ToList());
        }
        public ActionResult Events()
        {
                String type= PersonAccount.getInstance().AuthenticationType;
                return View(db.Event.ToList().Where(a => AccountGroupParserController.EventMatcher(a, type, pupile.Class_Number)).ToList());
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