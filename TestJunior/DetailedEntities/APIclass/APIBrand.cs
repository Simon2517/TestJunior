using System.Collections.Generic;

namespace TestJunior.DetailedEntities
{
    public class PaginatedBrand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<int> ProductIds { get; set; }

    }

    public class APIBrandDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumOfProducts { get; set; }
        public int NumOfInforequests { get; set; }
        public IEnumerable<PolishedProduct> ListOfProds { get; set; } = new List<PolishedProduct>();
        public IEnumerable<APICategoryWithNumOfProds> ListOfCategs { get; set; } = new List<APICategoryWithNumOfProds>();
    }
}
