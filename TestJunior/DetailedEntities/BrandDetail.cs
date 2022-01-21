using System.Collections.Generic;
using System.Linq;

namespace TestJunior.DetailedEntities
{
    public class BrandDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumOfProducts { get; set; }
        public int NumOfInforequests { get; set; }
        public IEnumerable<PolishedProduct> ListOfProds { get; set; } = new List<PolishedProduct>();
        public IEnumerable<Category> ListOfCategs { get; set; }= new List<Category>();
    }
}
