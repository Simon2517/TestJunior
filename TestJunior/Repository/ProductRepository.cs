using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestJunior.DetailedEntities;

namespace TestJunior.Repository
{

    public class ProductRepository : IRepository<Product>
    {
        private readonly DatabaseContext _ctx;
        public ProductRepository(DatabaseContext ctx)
        {
            _ctx = ctx;


        }

        public async Task<int> deleteAsync(int id)
        {
            int result = 0;
            using IDbContextTransaction transaction = _ctx.Database.BeginTransaction();
            try
            {
                var product = _ctx.Product.FirstOrDefault(x => x.ProductId == id);
                product.isDeleted = true;

                await _ctx.ProductCategories
                        .Where(pc => pc.ProductId == id)
                        .UpdateFromQueryAsync(x => new ProductCategories { isDeleted = true });

                await _ctx.InfoRequest
                        .Where(info => info.ProductId == id)
                        .UpdateFromQueryAsync(x => new InfoRequest { isDeleted = true });

                await _ctx.InfoRequestReply
                        .Where(reply => reply.InfoRequest.ProductId == id)
                            .UpdateFromQueryAsync(x => new InfoRequestReply { isDeleted = true });

                _ctx.Update(product);
                result=_ctx.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            return result ;
        }

        public IQueryable<Product> GetAll()
        {
            return _ctx.Product.AsQueryable();
        }

        public IQueryable<Product> GetById(int id)
        {
            return _ctx.Product.Where(p=>p.ProductId == id);
        }

        public int add(Product product)
        {
            _ctx.Product.Add(product);
            _ctx.SaveChanges();
            return product.ProductId;
        }

        public int update(Product product)
        {
            _ctx.Product.Update(product);
            return product.ProductId;
        }
    }
}
