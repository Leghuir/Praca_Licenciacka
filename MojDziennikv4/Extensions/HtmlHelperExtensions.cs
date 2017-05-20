using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Newtonsoft.Json;
using MojDziennikv4.Models;

namespace MojDziennikv4.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static HtmlString HtmlConvertToJson(this HtmlHelper htmlHelper, object model)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
            return new HtmlString(JsonConvert.SerializeObject(model, settings));
        }
        public static MvcHtmlString BuildSortableLink(this HtmlHelper htmlHelper, string fieldName,
            string actionName,string sortFiled,QueryOptions queryOptions)
        {
            var UrlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var isCurrentSortFiled = queryOptions.SortFiled == sortFiled;
            return new MvcHtmlString(string.Format("<a href\"{0}\" > {1} {2}</a>",
                UrlHelper.Action(actionName, new
                {
                    SortFiled = sortFiled,
                    SortOrder = (isCurrentSortFiled && queryOptions.Sortorder == SortOrder.ASC) ? SortOrder.DESC : SortOrder.ASC

                }), fieldName, BuildSortIcon(isCurrentSortFiled, queryOptions)));
        }
        public static string BuildSortIcon(bool isCurrentSortField, QueryOptions queryOptions)
        {
            string sortIcon = "sort";
            if(isCurrentSortField)
            {
                sortIcon += "-by-alphabet";
                if (queryOptions.Sortorder == SortOrder.DESC)
                    sortIcon += "-alt";
            }
            return String.Format("<span class=\"{0} {1}{2}\"></span>","glyphicon","glyphicon-",sortIcon);
        }
    }
}