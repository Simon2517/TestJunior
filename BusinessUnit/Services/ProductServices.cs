using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessUnit.Services.Interfaces;
using BusinessUnit.ViewModels;
using DataLayer;
using DataLayer.DetailedEntities;
using DataLayer.Repository;

namespace BusinessUnit.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IRepository<Product> _Productrepo;
        public ProductServices(IRepository<Product> _productrepo)
        {
            _Productrepo = _productrepo;
        }

        /// <summary>
        /// filters product by brand name
        /// </summary>
        /// <param name="brandName"></param>
        /// <returns>an iQueriable of products filtered by brand name</returns>
        public IQueryable<Product> FilterProducts(string brandName)
        {
            return _Productrepo.GetAll()
                        .Where(x => x.Brand.BrandName == brandName);
        }

        /// <summary>
        /// orders products by property selected
        /// </summary>
        /// <param name="order">represents the order by property</param>
        /// <param name="asc_desc">representes asc&desc by default true represents asc</param>
        /// <param name="brandName"></param>
        /// <returns>
        /// an order and filtered list if brandName is not empty
        /// otherwise an ordered list
        /// </returns>
        public IQueryable<Product> OrderedProducts(int order = 0, bool asc_desc = true, string brandName = "")
        {
            IQueryable<Product> prods;
            if (string.IsNullOrEmpty(brandName))
                prods = _Productrepo.GetAll();
            else
                prods = FilterProducts(brandName);
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
                        return prods.OrderBy(x => x.Brand.BrandName).ThenBy(x => x.Name);
                    else
                        return prods.OrderByDescending(x => x.Brand.BrandName).ThenBy(x => x.Name);

            }

        }

        /// <summary>
        /// creates a paginated list of ordered and filtered products
        /// </summary>
        /// <param name="pagenumber">the page we are at</param>
        /// <param name="pagesize">the total number of products in that page</param>
        /// <param name="order">the order of which the list will be ordered</param>
        /// <param name="asc_desc">the ascendant and descendant option of the order</param>
        /// <param name="brandName">the name of the brand used to filter the list of products</param>
        /// <returns>a paginated list</returns>
        public PaginatedList<PaginatedProduct> ListOfProducts(int pagenumber, int pagesize, int order, bool asc_desc, string brandName)
        {
            if (pagenumber <= 0 || pagesize <= 0)
                return null;

            IQueryable<PaginatedProduct> Prods = OrderedProducts(order, asc_desc, brandName)
                .Select(prod => new PaginatedProduct
                {
                    Id = prod.ProductId,
                    Name = prod.Name,
                    BrandName = prod.Brand.BrandName,
                    ShortDescription = prod.ShortDescription,
                    Price = prod.Price,
                    Categories = prod.ProdsCategories.Select(pc => pc.Category.Name)
                });
            return PaginatedList<PaginatedProduct>.Create(Prods, pagenumber, pagesize);
        }

        /// <summary>
        /// get detail of Product
        /// </summary>
        /// <param name="id">id of the product</param>
        /// <returns></returns>
        public IQueryable<APIProductDetail> ProductDetail(int id)
        {

            var Product = _Productrepo.GetById(id)
                .Select(prod => new APIProductDetail
                {
                    ProductId = prod.ProductId,
                    ProductName = prod.Name,
                    BrandName = prod.Brand.BrandName,
                    Categories = prod.ProdsCategories.Select(prodCat => new APICategory
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


        /// <summary>
        /// delete of product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> DeleteProductAsync(int id)
        {
            if (id <= 0)
                return 0;
            else
                return await _Productrepo.deleteAsync(id);

        }

        /// <summary>
        /// creates a product object binding its categories if any are selected
        /// </summary>
        /// <param name="productModel"></param>
        /// <returns></returns>
        public int AddProduct(APIProductWithCategories productModel)
        {
            Product product = productModel.Product;
            if (productModel.categoriesSelected.Count > 0)
            {
                List<ProductCategories> categories = new List<ProductCategories>();
                foreach (var x in productModel.categoriesSelected)
                {
                    categories.Add(new ProductCategories { CategoryId = x, ProductId = productModel.Product.ProductId });
                }
                product.ProdsCategories = categories;
            }
            return _Productrepo.add(product);
        }

        /// <summary>
        /// creates a viewModel by getting a product and binds its categories to a list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public APIProductWithCategories GetSingleProduct(int id)
        {
            var product = _Productrepo.GetById(id);
            var categories = product.SelectMany(x => x.ProdsCategories.Select(x => x.CategoryId)).ToList();
            var productViewModel = new APIProductWithCategories()
            {
                Product = product.FirstOrDefault(),
                categoriesSelected = categories
            };
            return productViewModel;
        }

        /// <summary>
        /// creates a product object with its categories starting from a product view model
        /// </summary>
        /// <param name="productModel"></param>
        /// <returns></returns>
        public int UpdateProduct(APIProductWithCategories productModel)
        {
            foreach (var x in productModel.categoriesSelected)
            {
                productModel.Product.ProdsCategories.Add(new ProductCategories
                {
                    CategoryId = x,
                });
            }
            return _Productrepo.update(productModel.Product);
        }
    }
}
