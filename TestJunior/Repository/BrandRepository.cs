using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace TestJunior.Repository
{

    public class BrandRepository : IRepository<Brand>
    {
        private DatabaseContext _ctx;

        public BrandRepository(DatabaseContext ctx)
        {
            _ctx = ctx;

        }

        public async Task<int> deleteAsync(int id)
        {
            int result = 0;
            using IDbContextTransaction transaction = _ctx.Database.BeginTransaction();
            try
            {
                var brand = _ctx.Brand.FirstOrDefault(x => x.Id == id);
                brand.isDeleted = true;

                await _ctx.InfoRequestReply
                        .Where(reply => reply.InfoRequest.Product.BrandId == id)
                            .UpdateFromQueryAsync(x => new InfoRequestReply { isDeleted = true });

                await _ctx.InfoRequest
                        .Where(info => info.Product.ProductId == id)
                        .UpdateFromQueryAsync(x => new InfoRequest { isDeleted = true });

                await _ctx.Product
                        .Where(p => p.BrandId == id)
                        .UpdateFromQueryAsync(x =>new Product { isDeleted=true});

                
                _ctx.Update(brand);
                result = _ctx.SaveChanges();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            return result;
        }

        public IQueryable<Brand> GetAll()
        {
            return _ctx.Brand.AsQueryable();

        }

        public IQueryable<Brand> GetById(int id)
        {
            return _ctx.Brand.Where(b => b.Id == id);
        }
        public int add(Brand brand)
        {
            var x = new Product
            {
                Name = null,
            };
            brand.Products.Append(x);
            _ctx.Brand.Add(brand);
            _ctx.SaveChanges();
            return brand.Id;
        }

        public int update(Brand brand)
        {

            _ctx.Brand.Update(brand);
            _ctx.SaveChanges();
            return brand.Id;
        }
    }
}
