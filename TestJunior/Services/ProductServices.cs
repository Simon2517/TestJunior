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

        public IQueryable<Product> OrderedProducts(int order=0,bool asc_desc=true)
        {
            var prods= _Productrepo.GetAll();
            switch (order)
            {
                case 1:
                    if (asc_desc)
                        return prods.OrderBy(x => x.Brand.BrandName);
                    else
                        return prods.OrderByDescending(x => x.Brand.BrandName);
                case 2:
                    if (asc_desc)
                        return prods.OrderBy(x => x.Name);
                    else
                        return prods.OrderByDescending(x => x.Name);
                case 3:
                    if (asc_desc)
                        return prods.OrderBy(x => x.Price);
                    else
                        return prods.OrderByDescending(x => x.Price);
                default:
                    if (asc_desc)
                        return prods.OrderBy(x => x.Brand.BrandName).ThenBy(x=> x.Name);
                    else
                        return prods.OrderByDescending(x => x.Brand.BrandName).ThenBy(x => x.Name);

            }

        }
        public PaginatedList<PaginatedProduct> ListOfProducts(int pagenumber, int pagesize,int order,bool asc_desc)
        {
            if (pagenumber <= 0 || pagesize <= 0)
                return null;
                
                IQueryable<PaginatedProduct> Prods = OrderedProducts(order,asc_desc)
                    .Select(prod => new PaginatedProduct
                    {
                        Id=prod.ProductId,
                        Name = prod.Name,
                        BrandName=prod.Brand.BrandName,
                        ShortDescription = prod.ShortDescription,
                        Price = prod.Price,
                        Categories=prod.ProdsCategories.Select(pc=>pc.Category.Name)
                    });
            return PaginatedList<PaginatedProduct>.Create(Prods, pagenumber, pagesize);
        }

        public IQueryable<APIProductDetail> ProductDetail(int id)
        {
            var Product = _Productrepo.GetAll()
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

            return Product;

        }
    }
}
