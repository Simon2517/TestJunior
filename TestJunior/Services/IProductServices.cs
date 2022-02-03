using System.Linq;
using TestJunior.DetailedEntities;

namespace TestJunior.Services
{
    public interface IProductServices
    {
        public PaginatedList<PaginatedProduct> ListOfProducts(int pagenumber, int pagesize/*,int order*/);
        public IQueryable<APIProductDetail> ProductDetail(int id);
    }
}
