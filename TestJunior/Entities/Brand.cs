using Newtonsoft.Json;
using System.Collections.Generic;

namespace TestJunior
{
    public class Brand
    {
        public int Id { get;  set; }
        public int? AccountId { get;  set; }
        public string BrandName { get;  set; }
        public string Description { get;  set; }
        public virtual Account Account { get;  set; }
        public virtual IEnumerable<Product> Products { get; private set; }=new List<Product>();


    }
}
