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
        public int currnetPage { get; set; }
        public int totalPage { get; set; }
        public int pageSize { get; set; }
        public T Searchitem{ get; set; }
        public QueryOptions()
        {
            currnetPage = 1;
            pageSize = 10;
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