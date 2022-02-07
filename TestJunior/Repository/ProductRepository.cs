using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            product.isDeleted = true;
            await _ctx.Database.ExecuteSqlRawAsync(@"UPDATE ProductCategories
                                                     SET isDeleted=1
                                                     WHERE ProductId= " + id);
            await _ctx.Database.ExecuteSqlRawAsync(@"UPDATE InfoRequest
                                                     SET isDeleted=1
                                                     FROM InfoRequest Ir join Product p On ir.ProductId=p.ProductId
                                                     WHERE  p.ProductId="+id);
            await _ctx.Database.ExecuteSqlRawAsync(@"UPDATE InfoRequestReply
                                                     SET isDeleted=1
                                                     FROM InfoRequestReply Irr join InfoRequest Ir on InforequestId=Ir.Id
                                                        join Product p On ir.ProductId=p.ProductId
                                                     WHERE  p.ProductId=" + id);




            //var product = _ctx.Product.Include(p => p.ProdsCategories)
            //                          .Include(p => p.InfoRequests)
            //                                .ThenInclude(ir => ir.InfoRequestReplies)
            //                          .FirstOrDefault(p => p.ProductId == id);
            //product.isDeleted = true;
            //product.ProdsCategories.ToList().ForEach(p =>p.isDeleted = true);
            //product.InfoRequests.ToList().ForEach(p=>p.isDeleted = true);
            //product.InfoRequests.SelectMany(p=>p.InfoRequestReplies).ToList().ForEach(p=>p.isDeleted = true);
            _ctx.SaveChanges();
            return 1;
        }

        public IQueryable<Product> GetAll()
        {
            return _ctx.Product.AsQueryable();
        }

        public IQueryable<Product> GetById(int id)
        {
            return _ctx.Product.Where(p=>p.ProductId == id);
        }

        Task<int> IRepository<Product>.add(Product entity)
        {
            throw new System.NotImplementedException();
        }

        Task<int> IRepository<Product>.update(Product entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
