using MojDziennikv4.Models;
using MojDziennikv4.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace MojDziennikv4.Filters
{
    public class BasicAuthenticationAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            bool logowanie = filterContext.HttpContext.Request.Url.ToString().Equals(@"http://localhost:9384/") || filterContext.HttpContext.Request.Url.ToString().Equals(@"http://localhost:9384/Login/Index");


            if (MojDziennikv4.Models.DAL.PersonAccount.getInstance().IsAuthenticated || logowanie)
            {
                filterContext.Principal = MojDziennikv4.Models.DAL.PersonAccount.getInstance();
                return;
            }
            else
            {
                HttpResponse Response = System.Web.HttpContext.Current.Response;
                Response.Redirect("/Login/Index",true);
                filterContext.Result = new HttpUnauthorizedResult("Niepoprawna nazwa użytkownika lub hasło");
                return;
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {

            filterContext.Result = new Tologin
            {
                CurrentResult = filterContext.Result
            };
            //if(filterContext.Result.Equals("Niepoprawna nazwa użytkownika lub hasło"))
            //filterContext.Result = new Tologin().Login("~/Views/Login/Index.cshtml", "", "", "");
            //else
            //{
            //    filterContext.Result = new Tologin().Login("~/Views/Home/Index.cshtml", "", "", "");
            //}
        }
    }
    class Tologin : ActionResult
    {
        public ActionResult CurrentResult { get; set; }
        public override void ExecuteResult(ControllerContext context)
        {
            try {
                CurrentResult.ExecuteResult(context);
            }
            catch(Exception e)
            {

            }
            
            var response = context.HttpContext.Response;
            if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
                response.AddHeader("WWW-Authenticate", "Basic");


        }
    }
}