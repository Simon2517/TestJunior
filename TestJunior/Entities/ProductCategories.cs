﻿using Newtonsoft.Json;

namespace TestJunior
{
    public class ProductCategories
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Product Product { get; set; }

    }
}
