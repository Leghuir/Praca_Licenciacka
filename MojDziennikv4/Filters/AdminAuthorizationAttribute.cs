using MojDziennikv4.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MojDziennikv4.Filters
{
    public class AdminAuthorizationAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var userIdentity = filterContext.HttpContext.User.Identity as PersonAccount;
            if(userIdentity ==null || !userIdentity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }
            if(userIdentity.AuthenticationType!="Administrator")
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }
        }
    }
}