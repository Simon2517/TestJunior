using System.Linq;

namespace TestJunior.Repository
{

    public class BrandRepository : IRepository<Brand>
    {
        private DatabaseContext _ctx;

        public BrandRepository(DatabaseContext ctx)
        {
            _ctx = ctx;
        }
        public IQueryable<Brand> GetAll()
        {
            return _ctx.Brand.AsQueryable();

        }

        public IQueryable<Brand> GetById(int id)
        {
            return _ctx.Brand.Where(b => b.Id == id);
        }
    }
}
