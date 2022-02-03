using Newtonsoft.Json;

namespace TestJunior
{
    /// <summary>
    /// class that represent the PoductCategories table of the DataBase
    /// </summary>
    public class ProductCategories
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        /// property that represents the foreign key between Category and ProductCategories
        public  Category Category { get; set; }
        /// property that represents the foreign key between Product and ProductCategories
        public  Product Product { get; set; }

    }
}
