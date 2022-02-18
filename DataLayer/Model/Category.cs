using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataLayer
{
    /// <summary>
    /// class that represent the Category table of the DataBase
    /// </summary>
    public class Category
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public string Name { get; set; }
        /// property that represents the foreign key between ProductCategories and Category
        public  ICollection<ProductCategories> ProdsCategories { get; set; } = new List<ProductCategories>();

    }
}
