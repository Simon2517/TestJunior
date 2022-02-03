using System.Linq;
using TestJunior.DetailedEntities;
using TestJunior.Repository;

namespace TestJunior.Services
{
    public class ProductServices:IProductServices
    {
        private readonly IRepository<Product> _Productrepo;
        public ProductServices(IRepository<Product> _productrepo)
        {
            _Productrepo = _productrepo;
        }
        public PaginatedList<PaginatedProduct> ListOfProducts(int pagenumber, int pagesize/*,int order*/)
        {
            if (pagenumber <= 0 || pagesize <= 0)
                return null;
                
                IQueryable<PaginatedProduct> Prods = _Productrepo.GetAll()
                    .Select(prod => new PaginatedProduct
                    {
                        Id=prod.ProductId,
                        Name = prod.Name,
                        ShortDescription = prod.ShortDescription,
                    });
            return PaginatedList<PaginatedProduct>.Create(Prods, pagenumber, pagesize);
        }


        public IQueryable<APIProductDetail> ProductDetail(int id)
        {
            var Products = _Productrepo.GetAll()
                .Select(prod => new APIProductDetail
                {
                    ProductId = prod.ProductId,
                    ProductName = prod.Name,

                    Catagories = prod.ProdsCategories.Select(prodCat => new APICategory
                    {
                        Id = prodCat.CategoryId,
                        Name = prodCat.Category.Name
                    }),
                    NumberOfGuestRequest = prod.InfoRequests.Where(x => x.UserId == null).Count(),
                    NumberOfUserRequest = prod.InfoRequests.Where(x => x.UserId != null).Count(),
                    Requests = prod.InfoRequests.Select(info => new PolishedInfoRequest
                    {
                        InfoId = info.Id,
                        Name = info.UserId == null ? info.Name : info.User.Name,
                        LastName = info.UserId == null ? info.LastName : info.User.LastName,
                        NumOfReplies = info.InfoRequestReplies.Count(),
                        LastDate = info.InfoRequestReplies.Max(ir => ir.InsertedDate)
                    })
                    .OrderByDescending(info => info.LastDate)
                })
                .Where(p => p.ProductId == id);

            return Products;

        }
    }
}
