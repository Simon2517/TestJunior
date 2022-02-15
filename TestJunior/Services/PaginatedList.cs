using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestJunior;

namespace TestJunior
{
    public class PaginatedList<T>
    {
        /// <summary>
        /// generic class used to paginate an object
        /// </summary>
        /// <param name="PageNumber"></param>
        /// <param name="PageSize"></param>
        /// <param name="TotalCount"></param>
        /// <param name="ListOfElements"></param>
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public List<T> ListOfElements { get; set;}

        public PaginatedList(List<T> items, int count, int pageNumber, int pageSize, int maxPages)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = count;
            TotalPages = maxPages;
            ListOfElements=items;
        }
        /// <summary>
        /// method used to create a paginated list of generic objects taken from an IQueryable
        /// </summary>
        /// <param name="source">the IQueryable from where the objects come from</param>
        /// <param name="pagenumber"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public static PaginatedList<T> Create(IQueryable<T> source, int pagenumber, int pagesize)
        {
            var count = source.Count();
            var maxPages=(int)Math.Ceiling(Convert.ToDecimal(count) / pagesize);
            var items = source.Skip((pagenumber - 1) * pagesize).Take(pagesize).ToList();
            return new PaginatedList<T>(items, count, pagenumber, pagesize, maxPages);
        }

        

    }
}
