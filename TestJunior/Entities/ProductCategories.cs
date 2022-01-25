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

        /// property that represents the foreign key betwen Category and ProductCategories
        public virtual Category Category { get; set; }
        /// property that represents the foreign key betwen Product and ProductCategories
        public virtual Product Product { get; set; }

    }
}
