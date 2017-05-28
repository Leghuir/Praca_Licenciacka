using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Dynamic;

namespace MojDziennikv4.Models
{
    public class QueryOptions<T> :IDisposable
    {
        public String SortFiled { get; set; }
        public SortOrder Sortorder { get; set; }

        public T Searchitem{ get; set; }
        public QueryOptions()
        {
            SortFiled = "1";
            Sortorder = SortOrder.ASC;

        }
        public String Sort
        {
            get
            {
                return String.Format("{0} {1}", SortFiled, Sortorder.ToString());
            }
        }

        public void Dispose()
        {
            

        }
    }
}