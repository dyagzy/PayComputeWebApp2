using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayCompute2
{
    public class EmployeeListPagination <T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public EmployeeListPagination(List<T> items,int count, int pageIndex, int pageSize )
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count /(double) pageSize);
            this.AddRange(items);
        }

        //To enable or disable paging button
        public bool IsPreviousPageAvailabe => PageIndex > 1;

        public bool IsNextPageAvailable => PageIndex < TotalPages;

        public static EmployeeListPagination<T> Create (IList<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items =  source.Skip((pageIndex-1) * pageSize).Take(pageSize).ToList();
            return new EmployeeListPagination<T>(items, count, pageSize, pageIndex);

        }

    }
}
