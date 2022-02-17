using BusinessUnit.ViewModels;
using System;
using System.Collections.Generic;

namespace DataLayer.DetailedEntities
{
    public class PaginatedRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BrandName { get; set; }
        public string ProductName { get; set; }
        public DateTime RequestedDate { get; set; }

    }

    public class APIRequestDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string RequestText { get; set; }
        public PolishedProduct Product { get; set; }
        public IEnumerable<APIReply> Replies { get; set; } = new List<APIReply>();
    }
}
