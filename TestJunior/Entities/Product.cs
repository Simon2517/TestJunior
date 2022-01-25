using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

namespace TestJunior
{
    public class Product
    {
        /// <summary>
        /// class that represent the Product table of the DataBase
        /// </summary>
        public int ProductId { get; set; }
        public int BrandId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public virtual Brand Brand { get; set; }
        /// property that represents the foreign key between ProductCategories and Product
        public virtual IEnumerable<ProductCategories> ProdsCategories { get; set; }=new List<ProductCategories>();
        /// property that represents the foreign key between InfoRequests and Product
        public virtual IEnumerable<InfoRequest> InfoRequests { get; set; }= new List<InfoRequest>();

    }
}
