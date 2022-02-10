using System.Collections.Generic;
using System.Linq;
using TestJunior.DetailedEntities;
using TestJunior.Repository;

namespace TestJunior.Services
{
    public class BrandServices:IBrandServices
    {
        private readonly IRepository<Brand> _Brandrepo;
        private readonly IRepository<Product> _Productrepo;
        public BrandServices(IRepository<Brand> brandrepo, IRepository<Product> _productrepo)
        {
            _Brandrepo = brandrepo;
            _Productrepo = _productrepo;
        }
        public PaginatedList<PaginatedBrand> ListOfBrands(int pagenumber, int pagesize, int order, bool asc_desc)
        {
            if (pagenumber <= 0 || pagesize <= 0)
                return null;
            IQueryable<PaginatedBrand> Brands = OrderedBrands(order,asc_desc)
                    .Select(brand => new PaginatedBrand
                    {
                        Id = brand.Id,
                        Name = brand.BrandName,
                        Description = brand.Description,
                        ProductIds = brand.Products.Select(ids => ids.ProductId)
                    });
            return PaginatedList<PaginatedBrand>.Create(Brands, pagenumber, pagesize);
        }


        public IQueryable<APIBrandDetail> BrandDetail(int id)
        {

            var Categs = _Productrepo.GetAll()
                    .SelectMany(p => p.ProdsCategories)
                    .Where(x => x.Product.BrandId == id)
                    .GroupBy(x => new { x.CategoryId, x.Category.Name })
                    .Select(y => new APICategoryWithNumOfProds
                    {
                        Id = y.Key.CategoryId,
                        Name = y.Key.Name,
                        NumOfProds = y.Count()
                    });

            var Brands = _Brandrepo.GetAll()
                .Select(brand => new APIBrandDetail
                {
                    Id = brand.Id,
                    Name = brand.BrandName,
                    NumOfProducts = brand.Products.Count(),
                    NumOfInforequests = brand.Products.SelectMany(prods => prods.InfoRequests).Count(),
                    ListOfProds = brand.Products.Select(prod => new PolishedProduct
                    {
                        ProductId = prod.ProductId,
                        ProductName = prod.Name,
                        NumOfInforequest = prod.InfoRequests.Count()
                    }),
                    ListOfCategs = Categs.ToList()
                            })
                    .Where(brand => brand.Id == id);
            return Brands;
            }

        public IQueryable<Brand> OrderedBrands(int order, bool asc_desc)
        {
            var prods = _Brandrepo.GetAll();
            switch (order)
            {
                case 1:
                    if (asc_desc)
                        return prods.OrderBy(x => x.BrandName);
                    else
                        return prods.OrderByDescending(x => x.BrandName);
                default:
                    if (asc_desc)
                        return prods.OrderBy(x => x.Id);
                    else
                        return prods.OrderByDescending(x => x.Id);

            }
        }

        public List<APIBrand> GetAllBrandNames()
        {
           
            return _Brandrepo.GetAll().Select(b => new APIBrand{
                Id=b.Id,
                Name= b.BrandName 
            
            }).ToList();
        }

        public Brand GetSingleBrand(int id)
        {
            return _Brandrepo.GetById(id).FirstOrDefault();

        }

        public int AddBrand(BrandViewModel entity)
        {
            Brand brand = entity.brand;
            List<ProductCategories> categories = new List<ProductCategories>();
            List<Product> prods = new List<Product>();
            foreach (var prod in entity.prodCategories)
            {
                categories.Clear();
                foreach (var cat in prod.categoriesSelected)
                {
                    categories.Add(new ProductCategories { ProductId = prod.Product.ProductId, CategoryId = cat });
                }
                prod.Product.ProdsCategories = categories;
                prods.Add(prod.Product);
                
            }
            brand.Products = prods;
            return _Brandrepo.add(brand);
        }
    }
}
