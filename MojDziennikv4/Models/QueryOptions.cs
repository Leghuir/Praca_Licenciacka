using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Dynamic;

namespace MojDziennikv4.Models
{
    public class QueryOptions
    {
        public String SortFiled { get; set; }
        public SortOrder Sortorder { get; set; }

        public QueryOptions()
        {
            SortFiled = "Login";
            Sortorder = SortOrder.ASC;

        }
        public String Sort
        {
            get
            {
                return String.Format("{0} {1}", SortFiled, Sortorder.ToString());
            }
        }
    }
}