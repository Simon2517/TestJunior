using Newtonsoft.Json;
using System.Collections.Generic;

namespace TestJunior
{
    /// <summary>
    /// class that represent the Category table of the DataBase
    /// </summary>
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        /// property that represents the foreign key between ProductCategories and Category
        public virtual IEnumerable<ProductCategories> ProdsCategories { get; set; } = new List<ProductCategories>();
        public int NumOfProducts { get; set; }

    }
}
