using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MojDziennikv4.Filters
{
    public class ValidationActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var modelState = actionContext.ModelState;
            if (modelState.IsValid)
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, modelState);
            
        }
    }
}