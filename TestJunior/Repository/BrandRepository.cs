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

        public void add(Brand entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> deleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<Brand> GetAll()
        {
            return _ctx.Brand.AsQueryable();

        }

        public IQueryable<Brand> GetById(int id)
        {
            return _ctx.Brand.Where(b => b.Id == id);
        }
        Task<int> IRepository<Brand>.add(Brand entity)
        {
            throw new System.NotImplementedException();
        }

        Task<int> IRepository<Brand>.update(Brand entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
