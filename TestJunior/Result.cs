using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestJunior
{
    public class Result<T>
    {
        public int Count { get; set; }
        public PaginatedList<T> list { get; set; }
    }
}
