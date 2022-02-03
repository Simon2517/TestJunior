using System.Collections.Generic;

namespace TestJunior.DetailedEntities
{
    public class PaginatedProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }

    }
    public class APIProductDetail
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public IEnumerable<APICategory> Catagories { get; set; } = new List<APICategory>();
        public int NumberOfGuestRequest { get; set; }
        public int NumberOfUserRequest { get; set; }
        public IEnumerable<PolishedInfoRequest> Requests { get; set; } = new List<PolishedInfoRequest>();
    }
}
