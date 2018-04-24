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
        public static MvcHtmlString BuildNextPerviousLinks( this HtmlHelper htmlHelper,QueryOptions<String> queryoptions,String actionName)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            return new MvcHtmlString(String.Format(
                "<nav>" +
                "<ul class=\"pager\">" +
                "<li class=\"left previous {0}\">{1}</</li>" +
                "<li class=\"next {2}\"> {3} <li> " +
                "</ul>" +
                "</nav>",
                IsPreviousDisabled(queryoptions),
                BuildPerviousLink(urlHelper, queryoptions, actionName),
                IsNextDisabled(queryoptions),
                BuildnextLink(urlHelper, queryoptions, actionName)
                ));
        }
        public static String IsPreviousDisabled(QueryOptions<String> queryOption)
        {
            return (queryOption.currnetPage ==1) ? "disabled": String.Empty;
        }
        public static String IsNextDisabled(QueryOptions<String> queryOption)
        {
            return (queryOption.currnetPage == queryOption.totalPage) ? "disabled" : String.Empty;
        }
        private static String BuildPerviousLink(UrlHelper urlHelper,QueryOptions<String> queryOptions,String actionName)
        {
            return String.Format("<a href=\"{0}\"><span aria-hidden=\"true\">&larr;</span>Poprzednia</a> ",
                urlHelper.Action(actionName, new
                {
                    Sortorder = queryOptions.Sortorder,
                    SortFiled = queryOptions.SortFiled,
                    currnetPage = queryOptions.currnetPage - 1,
                    pageSize= queryOptions.pageSize

                }));
        }
        private static String BuildnextLink(UrlHelper urlHelper, QueryOptions<String> queryOptions, String actionName)
        {
            return String.Format("<a href=\"{0}\">Następna<span aria-hidden=\"true\">&rarr;</span></a> ",
                urlHelper.Action(actionName, new
                {
                    Sortorder = queryOptions.Sortorder,
                    SortFiled = queryOptions.SortFiled,
                    currnetPage = queryOptions.currnetPage + 1,
                    pageSize = queryOptions.pageSize

                }));
        }
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
            string actionName,string sortFiled,QueryOptions<String> queryOptions)
        {
            var UrlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var isCurrentSortFiled = queryOptions.SortFiled == sortFiled;
            return new MvcHtmlString(string.Format("<a href=\"{0}\" > {1} {2}</a>",
                UrlHelper.Action(actionName, new
                {
                    SortFiled = sortFiled,
                    SortOrder = (isCurrentSortFiled && queryOptions.Sortorder == SortOrder.ASC) ? SortOrder.DESC : SortOrder.ASC

                }), fieldName, BuildSortIcon(isCurrentSortFiled, queryOptions)));
        }
        public static string BuildSortIcon(bool isCurrentSortField, QueryOptions<String> queryOptions)
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
        public static MvcHtmlString BuildSearchableLink(this HtmlHelper htmlHelper, string fieldName,string fieldVaule,
            string actionName, string searchFiled, QueryOptions<String> queryOptions)
        {
            var UrlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            return new MvcHtmlString(string.Format("<form action=\"{0}\"> <input type=\"text\" name=\"{2}\" placeholder=\"{1}\" class=\"form-control\" value=\" \"><br><input type=\"submit\" value=\"Szukaj\" class=\"btn btn - default\" ></form>",
                UrlHelper.Action(actionName, new
                {
                    SearchFiled = searchFiled
                }), fieldVaule, fieldName));
        }
    }
}