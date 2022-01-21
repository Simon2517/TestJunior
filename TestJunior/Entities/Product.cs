using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

namespace TestJunior
{
    public class Product
    {
        public int ProductId { get; set; }
        public int BrandId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual IEnumerable<ProductCategories> ProdsCategories { get; set; }=new List<ProductCategories>();
        public virtual IEnumerable<InfoRequest> InfoRequests { get; set; }= new List<InfoRequest>();

    }
}
