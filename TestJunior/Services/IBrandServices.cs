using System.Collections.Generic;
using System.Linq;
using TestJunior.DetailedEntities;

namespace TestJunior.Services
{
    public interface IBrandServices
    {
        public IQueryable<Brand> OrderedBrands(int order, bool asc_desc);
        public PaginatedList<PaginatedBrand> ListOfBrands(int pagenumber, int pagesize,int order, bool asc_desc);
        public IQueryable<APIBrandDetail> BrandDetail(int id);
        public List<APIBrand> GetAllBrandNames();
        public int AddBrand(BrandViewModel brand);
        public int UpdateBrand(Brand brand);
        public Brand GetSingleBrand(int id);
    }
}
