using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.DetailedEntities;

namespace BusinessUnit.Services.Interfaces
{
    public interface IProductServices
    {
        public IQueryable<Product> OrderedProducts(int order, bool asc_desc, string brandName);
        public IQueryable<Product> FilterProducts(string brandName);
        public PaginatedList<PaginatedProduct> ListOfProducts(int pagenumber, int pagesize, int order, bool asc_desc, string brandName);
        public IQueryable<APIProductDetail> ProductDetail(int id);
        public APIProductWithCategories GetSingleProduct(int id);
        public Task<int> DeleteProductAsync(int id);
        public int AddProduct(APIProductWithCategories product);
        public int UpdateProduct(APIProductWithCategories product);
    }
}
