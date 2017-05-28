using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MojDziennikv4.Models.DAL;
using MojDziennikv4.Models;

namespace MojDziennikv4.Filters
{
    public class PupilAuthorizationAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var userIdentity = filterContext.HttpContext.User.Identity as PersonAccount;
            if (userIdentity == null || !userIdentity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }
            if (userIdentity.AuthenticationType != AccountType.Uczen.ToString() && userIdentity.AuthenticationType != AccountType.Opiekun.ToString())
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }
        }
    }
}
