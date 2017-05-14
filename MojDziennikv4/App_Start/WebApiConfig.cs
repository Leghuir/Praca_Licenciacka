using MojDziennikv4.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MojDziennikv4.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new ValidationActionFilterAttribute());
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controler}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );
        }
    }
}