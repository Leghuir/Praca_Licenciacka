﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MojDziennikv4.Models;
using System.Threading;
using MojDziennikv4.Models.DAL;

namespace MojDziennikv4.Controllers
{
    public class LoginController : Controller
    {


        // GET: Login

        private MojDziennikEntities db = new MojDziennikEntities();
        public ActionResult Index(String frm_login, String frm_password, String sel_type)
        {
            Thread.CurrentPrincipal = PersonAccount.getInstance();
            if (System.Web.HttpContext.Current != null)
            {
                System.Web.HttpContext.Current.User = PersonAccount.getInstance(); ;
            }
            ViewBag.hlp = new LoginHelper(db.Account.ToList(), frm_password, frm_login, sel_type);
            return View(db.Account.ToList());
        }
    }
        
}
