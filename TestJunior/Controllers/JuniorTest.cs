using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestJunior;
using TestJunior.DetailedEntities;

namespace TestJunior.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JuniorTest : ControllerBase
    {

        private readonly DatabaseContext _ctx;

        public JuniorTest(DatabaseContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("Products/{PageNumber}/{PageSize}")]

        public IActionResult GetAllProducts(int PageNumber, int PageSize)
        {
            var Products = _ctx.Product
                    .Select(prod => new PolishedProduct
                    {
                        ProductId = prod.ProductId,
                        ProductName = prod.Name,
                        ShortDescription = prod.ShortDescription
                    });

            return Ok(PaginatedList<PolishedProduct>.Create(Products, PageNumber, PageSize));
        }

        [HttpGet("Brands/{PageNumber}/{PageSize}")]

        public IActionResult GetAllBrand(int PageNumber, int PageSize)
        {
            var Brands = _ctx.Brand
                    .Select(brand => new PolishedBrand
                    {

                        Name = brand.BrandName,
                        Description = brand.Description,
                        ProductIds = brand.Products.Select(ids => ids.ProductId)
                        
                    });


            return Ok(PaginatedList<PolishedBrand>.Create(Brands, PageNumber, PageSize));
        }

        [HttpGet("BrandDetail/{id}")]
        public IActionResult GetBrandDetail(int id)
        {
            var Categs= from prod in _ctx.Product
                        join prodCat in _ctx.ProductCategories on prod.ProductId equals prodCat.ProductId
                        join cat in _ctx.Category on prodCat.CategoryId equals cat.Id
                        where prod.BrandId == id
                        group cat by new { Id=cat.Id, Name=cat.Name } into Categories
                        select new Category{
                            Id=Categories.Key.Id,
                            Name=Categories.Key.Name,
                            NumOfProducts=Categories.Count()};
            
            var Brands = _ctx.Brand
                            .Include(b => b.Products)
                                .ThenInclude(p => p.ProdsCategories)
                                    .ThenInclude(pc => pc.Category)
                            .Select(brand => new 
                            {
                                Id = brand.Id,
                                Name = brand.BrandName,
                                NumOfProducts = brand.Products.Count(),
                                NumOfInforequests = brand.Products.SelectMany(prods => prods.InfoRequests).Count(),
                                ListOfProds = brand.Products.Select(prod => new 
                                {
                                    ProductId = prod.ProductId,
                                    ProductName = prod.Name,
                                    NumOfInforequest = prod.InfoRequests.Count()
                                }),
                                ListOfCategs = Categs.ToList()
                            })
                            .Where(brand=>brand.Id == id);

            
            return Ok(Brands);
        }

        [HttpGet("ProductDetail/{id}")]
        //
        public IActionResult GetProductDetail(int id)
        {
            var Products = _ctx.Product
                    .Include(product => product.ProdsCategories)
                          .ThenInclude(pc => pc.Category)
                    .Include(p => p.InfoRequests)
                          .ThenInclude(ir => ir.User)
                    .Select(prod => new ProductDetail
                    {
                        ProductId = prod.ProductId,
                        ProductName = prod.Name,

                        Catagories = prod.ProdsCategories.Select(prodCat => new Category
                        {
                            Id=prodCat.CategoryId,
                            Name=prodCat.Category.Name
                        }),
                        NumberOfGuestRequest = prod.InfoRequests.Where(x => x.UserId == null).Count(),
                        NumberOfUserRequest = prod.InfoRequests.Where(x => x.UserId != null).Count(),
                        Requests=prod.InfoRequests.Select(info=>new PolishedInfoRequest
                        {
                            InfoId=info.Id,
                            Name=info.UserId==null ? info.Name : info.User.Name,
                            LastName=info.UserId==null ? info.LastName : info.User.LastName,
                            NumOfReplies=info.InfoRequestReplies.Count(),
                            LastDate=info.InfoRequestReplies.Max(ir=>ir.InsertedDate)
                        })
                        .OrderByDescending(info => info.LastDate)
                    })
                    .Where(p => p.ProductId == id);

            return Ok(Products);
        }

        [HttpGet("InfoRequestDetail/{id}")]
        public IActionResult GetInfoRequestDetail(int id)
        {
            var InfoRequests =_ctx.InfoRequest
                                .Include(info=>info.Product)
                                    .ThenInclude(p=>p.Brand)
                                .Include(info=>info.InfoRequestReplies)
                                    .ThenInclude(reply=>reply.Account)
                                .Include(info=>info.Nation)

                                .Select(info=> new RequestDetail
                                {
                                    Id=info.Id,
                                    Name = info.Name+" "+info.LastName,
                                    Email=info.Email,
                                    Address=info.City+"("+info.PostalCode+"),"+info.Nation.Name,
                                    Product=new PolishedProduct
                                    {
                                        ProductId=info.ProductId,
                                        ProductName=info.Product.Name,
                                        Brandname=info.Product.Brand.BrandName,
                                    },
                                    Replies=info.InfoRequestReplies.OrderByDescending(info=>info.InsertedDate)
                                    .Select(rep=>new APIReply
                                    {
                                        Id=rep.Id,
                                        Name=rep.Account.AccountType==1?info.Product.Brand.BrandName:info.Name+" "+info.LastName,
                                        ReplyText=rep.ReplyText
                                    }),
                                   
                                })
                                .Where(info=>info.Id==id);


            return Ok(InfoRequests);
        }

        [HttpGet("Test/{id}")]
        public IActionResult Test(int id)
        {

            var Categs = _ctx.ProductCategories
                .Where(x => x.Product.BrandId == id)
                .GroupBy(x => new { x.CategoryId, x.Category.Name })
                .Select(y => new
                {
                    Id = y.Key.CategoryId,
                    Name = y.Key.Name,
                    count=y.Count()
                });

            var Brands = _ctx.Brand
                .Include(b => b.Products)
                    .ThenInclude(p => p.ProdsCategories)
                        .ThenInclude(pc => pc.Category)
                .Select(brand => new
                {
                    Id = brand.Id,
                    Name = brand.BrandName,
                    NumOfProducts = brand.Products.Count(),
                    NumOfInforequests = brand.Products.SelectMany(prods => prods.InfoRequests).Count(),
                    ListOfProds = brand.Products.Select(prod => new
                    {
                        ProductId = prod.ProductId,
                        ProductName = prod.Name,
                        NumOfInforequest = prod.InfoRequests.Count()
                    }),
                    ListOfCategs = Categs.ToList()
                })
                .Where(brand => brand.Id == id);

            return Ok(Brands);
        }
    }
}


