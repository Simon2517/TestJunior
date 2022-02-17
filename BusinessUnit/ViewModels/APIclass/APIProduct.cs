using BusinessUnit.ViewModels;
using System.Collections.Generic;

namespace DataLayer.DetailedEntities
{
    public class PaginatedProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public decimal Price { get; set; }
        public string BrandName { get; set; }
        public IEnumerable<string> Categories { get; set; }
    }
    public class APIProductDetail
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string BrandName { get; set; }
        public IEnumerable<APICategory> Categories { get; set; } = new List<APICategory>();
        public int NumberOfGuestRequest { get; set; }
        public int NumberOfUserRequest { get; set; }
        public IEnumerable<PolishedInfoRequest> Requests { get; set; } = new List<PolishedInfoRequest>();
    }
    public class APIProductWithCategories
    {
        public Product Product { get; set; }
        public List<int> categoriesSelected { get; set; }= new List<int>();
    }
}
