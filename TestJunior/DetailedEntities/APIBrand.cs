using System.Collections.Generic;

namespace TestJunior.DetailedEntities
{
    public abstract class APIBrand
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PaginatedBrand:APIBrand
    {
        public string Description { get; set; }
    }

    public class APIBrandDetail : APIBrand
    {
        public int NumOfProducts { get; set; }
        public int NumOfInforequests { get; set; }
        public IEnumerable<PolishedProduct> ListOfProds { get; set; } = new List<PolishedProduct>();
        public IEnumerable<Category> ListOfCategs { get; set; } = new List<Category>();
    }
}
