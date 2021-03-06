using Newtonsoft.Json;
using System.Collections.Generic;

namespace TestJunior
{
    /// <summary>
    /// class that represent the Brand table of the DataBase
    /// </summary>
    public class Brand
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public string BrandName { get;  set; }
        public int AccountId { get; set; }
        public string Description { get;  set; }
        /// property that represents the foreign key between Account and Brand
        public  Account Account { get;  set; }
        /// property that represents the foreign key between Products and Brand
        public  IEnumerable<Product> Products { get;  set; }=new List<Product>();


    }
}
