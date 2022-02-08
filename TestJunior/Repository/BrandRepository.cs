using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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
            var brand = _ctx.Brand.FirstOrDefault(x => x.Id == id);
            if (brand != null)
            {
                brand.isDeleted = true;

                await _ctx.Database.ExecuteSqlRawAsync(@"UPDATE Product
                                                         SET isDeleted=1
                                                         WHERE BrandId= " + id);

                await _ctx.Database.ExecuteSqlRawAsync(@"UPDATE ProductCategories
                                                         SET isDeleted=1
                                                         FROM ProductCategories prodCat 
                                                            join Product p on p.ProductId=prodCat.ProductId
                                                         WHERE p.BrandId= " + id);

                await _ctx.Database.ExecuteSqlRawAsync(@"UPDATE InfoRequest
                                                         SET isDeleted=1
                                                         FROM InfoRequest infoRequest 
                                                            join Product p On infoRequest.ProductId=p.ProductId
                                                         WHERE  p.BrandId=" + id);

                await _ctx.Database.ExecuteSqlRawAsync(@"UPDATE InfoRequestReply
                                                         SET isDeleted=1
                                                         FROM InfoRequestReply infoReply 
                                                            join InfoRequest infoRequest on infoReply.InforequestId=infoRequest.Id
                                                            join Product p On infoRequest.ProductId=p.ProductId
                                                         WHERE  p.BrandId=" + id);
            }

            return _ctx.SaveChanges();
        }

        public IQueryable<Brand> GetAll()
        {
            return _ctx.Brand.AsQueryable();

        }

        public IQueryable<Brand> GetById(int id)
        {
            return _ctx.Brand.Where(b => b.Id == id);
        }
        public int add(Brand entity)
        {
            _ctx.Brand.Add(entity);
            //_ctx.Product.AddRange(entity.Products);
            return _ctx.SaveChanges();
        }

        public int update(Brand entity)
        {
            _ctx.Brand.Update(entity);
            return _ctx.SaveChanges();

        }
    }
}
