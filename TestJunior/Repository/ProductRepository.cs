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

            var product = _ctx.Product.FirstOrDefault(x => x.ProductId == id);
            if (product != null)
            {
                product.isDeleted = true;
                await _ctx.Database.ExecuteSqlRawAsync(@"UPDATE ProductCategories
                                                     SET isDeleted=1
                                                     WHERE ProductId= " + id);

                await _ctx.Database.ExecuteSqlRawAsync(@"UPDATE InfoRequest
                                                     SET isDeleted=1
                                                     FROM InfoRequest infoRequest join Product p On infoRequest.ProductId=p.ProductId
                                                     WHERE  p.ProductId=" + id);

                await _ctx.Database.ExecuteSqlRawAsync(@"UPDATE InfoRequestReply
                                                     SET isDeleted=1
                                                     FROM InfoRequestReply infoReply 
                                                        join InfoRequest infoRequest on infoReply.InforequestId=infoRequest.Id
                                                        join Product p On infoRequest.ProductId=p.ProductId
                                                     WHERE  p.ProductId=" + id);
            }

            return _ctx.SaveChanges();
        }

        public IQueryable<Product> GetAll()
        {
            return _ctx.Product.AsQueryable();
        }

        public IQueryable<Product> GetById(int id)
        {
            return _ctx.Product.Where(p=>p.ProductId == id);
        }

        public int add(Product entity)
        {
            IDbContextTransaction transaction=_ctx.Database.BeginTransaction();
            try
            {
                _ctx.Product.Add(entity);
                _ctx.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            return entity.ProductId;
        }

        public int update(Product entity)
        {
            IDbContextTransaction transaction = _ctx.Database.BeginTransaction();
            try
            {
                _ctx.Product.Update(entity);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            return entity.ProductId;
        }
    }
}
