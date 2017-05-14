using MojDziennikv4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MojDziennikv4.Controllers
{
    public class ParticalPupilInClassController : Controller
    {
        private MojDziennikEntities db = new MojDziennikEntities(); 
        // GET: ParticalPupilInClass
        public ActionResult Index(String Klasa)
        {
            return PartialView(db.Pupil.Where(a => a.Class_Number.Equals(Klasa)).ToList());
        }
    }
}