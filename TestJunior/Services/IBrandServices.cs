using System.Linq;
using TestJunior.DetailedEntities;

namespace TestJunior.Services
{
    public interface IBrandServices
    {
        public PaginatedList<PaginatedBrand> ListOfBrands(int pagenumber, int pagesize/*,int order*/);
        public IQueryable<APIBrandDetail> BrandDetail(int id);
    }
}
