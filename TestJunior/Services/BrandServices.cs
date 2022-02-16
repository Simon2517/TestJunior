using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        /// <summary>
        /// creates a paginated list of ordered and filtered brands
        /// </summary>
        /// <param name="pagenumber">the page we are at</param>
        /// <param name="pagesize">the total number of products in that page</param>
        /// <param name="brandName">the name of the brand searched</param>
        /// <returns>a paginated listof brands</returns>
        public PaginatedList<PaginatedBrand> ListOfBrands(int pagenumber, int pagesize, string brandName)
        {
            if (pagenumber <= 0 || pagesize <= 0)
                return null;
            IQueryable<PaginatedBrand> Brands = FilterBrands(brandName)
                    .Select(brand => new PaginatedBrand
                    {
                        Id = brand.Id,
                        Name = brand.BrandName,
                        Description = brand.Description,
                        ProductIds = brand.Products.Select(ids => ids.ProductId)
                    });
            return PaginatedList<PaginatedBrand>.Create(Brands, pagenumber, pagesize);
        }

        /// <summary>
        /// get brand detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brandName"></param>
        /// <returns></returns>
        public IQueryable<Brand> FilterBrands(string brandName)
        {
            string _search=brandName ?? "";
            var prods = _Brandrepo.GetAll();
            if(string.IsNullOrEmpty(brandName))
                return prods;
            else
                return prods.Where(b=>b.BrandName.ToLower().Contains(_search.ToLower()));
        }

        /// <summary>
        /// get all brands
        /// </summary>
        /// <returns></returns>
        public List<APIBrand> GetAllBrandNames()
        {
           
            return _Brandrepo.GetAll().Select(b => new APIBrand{
                Id=b.Id,
                Name= b.BrandName             
            }).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a brand with its account properties</returns>
        public Brand GetSingleBrand(int id)
        {
            var brand = _Brandrepo.GetById(id).Select(b=>new Brand
            {
                Id=b.Id,
                BrandName=b.BrandName,
                Description=b.Description,
                Account=new Account {
                    Id=b.AccountId,
                    Email=b.Account.Email,
                    Password=b.Account.Password,
                    AccountType=b.Account.AccountType,
                }
            }).FirstOrDefault();
            
            return brand;

        }

        /// <summary>
        /// creates a single brand entity
        /// bounding for each if its products, if there's any,
        /// their categories if any are selected
        /// </summary>
        /// <param name="brandModel">the brand with its products</param>
        /// <returns>the number of database entities affected</returns>
        public int AddBrand(BrandViewModel brandModel)
        {
            Brand brand = brandModel.brand;
            brand.Account.AccountType = 2;

            if(brandModel.prodCategories.Count > 0)
            {
                List<ProductCategories> categories = new List<ProductCategories>();

                foreach (APIProductWithCategories prod in brandModel.prodCategories)
                {
                    if(prod.categoriesSelected.Count > 0)
                    {
                        categories.Clear();
                        foreach (int cat in prod.categoriesSelected)
                        {
                            categories.Add(new ProductCategories { 
                                ProductId = prod.Product.ProductId,
                                CategoryId = cat });
                        }
                    }

                    prod.Product.ProdsCategories = categories;
                }

                brand.Products = brandModel.prodCategories.Select(prods => prods.Product).ToList();
            }
            return _Brandrepo.add(brand);
        }

        public int UpdateBrand(Brand brand)
        {
            return _Brandrepo.update(brand);
        }

        public async Task<int> DeleteBrandAsync(int id)
        {
            if (id <= 0)
                return 0;
            else
                return await _Brandrepo.deleteAsync(id);

        }
    }
}
