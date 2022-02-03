using System.Linq;

namespace TestJunior.Repository
{

    public class ProductRepository : IRepository<Product>
    {
        private readonly DatabaseContext _ctx;

        public ProductRepository(DatabaseContext ctx)
        {
            _ctx = ctx;
        }
        public IQueryable<Product> GetAll()
        {
            return _ctx.Product.AsQueryable();
        }

        public IQueryable<Product> GetById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
