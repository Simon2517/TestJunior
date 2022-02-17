using DataLayer;
using System.Collections.Generic;

namespace BusinessUnit.ViewModels
{
    public class ProductDetail
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public IEnumerable<Category> Catagories { get; set; } = new List<Category>();
        public int NumberOfGuestRequest { get; set; }
        public int NumberOfUserRequest { get; set; }
        public IEnumerable<PolishedInfoRequest> Requests { get; set; } = new List<PolishedInfoRequest>();


    }
}
