using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.DetailedEntities;

namespace BusinessUnit.Services.Interfaces
{
    public interface IBrandServices
    {
        public IQueryable<Brand> FilterBrands(string search);
        public PaginatedList<PaginatedBrand> ListOfBrands(int pagenumber, int pagesize, string search);
        public IQueryable<APIBrandDetail> BrandDetail(int id);
        public List<APIBrand> GetAllBrandNames();
        public int AddBrand(BrandViewModel brand);
        public int UpdateBrand(Brand brand);
        public Brand GetSingleBrand(int id);
        public Task<int> DeleteBrandAsync(int id);
        public bool isEmailValid(string email);
    }
}
