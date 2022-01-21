using Newtonsoft.Json;
using System.Collections.Generic;

namespace TestJunior
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<ProductCategories> ProdsCategories { get; set; } = new List<ProductCategories>();
        public int NumOfProducts { get; set; }

    }
}
