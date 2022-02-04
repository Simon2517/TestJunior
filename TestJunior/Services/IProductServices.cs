using System.Linq;
using TestJunior.DetailedEntities;

namespace TestJunior.Services
{
    public interface IProductServices
    {
        public IQueryable<Product> OrderedProducts(int order,bool asc_desc);
        public PaginatedList<PaginatedProduct> ListOfProducts(int pagenumber, int pagesize, int order, bool asc_desc);
        public IQueryable<APIProductDetail> ProductDetail(int id);
    }
}
