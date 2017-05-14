using MojDziennikv4.Filters;
using System.Web;
using System.Web.Mvc;

namespace MojDziennikv4
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new BasicAuthenticationAttribute());
        }
    }
}
