using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class PagedDataModel<T>
    {
        public IList<T> Items { get; set; }

        public int PageIndex { get; set; }

        public int TotalPage { get; set; }

        public int TotalCount { get; set; }
    }
}
