using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TestJunior.Repository
{

    public class CategoryRepository : IRepository<Category>
    {
        private DatabaseContext _ctx;
        private IDbContextTransaction transaction;




        public CategoryRepository(DatabaseContext ctx)
        {
            _ctx = ctx;
            transaction = _ctx.Database.BeginTransaction();

        }

        public int add(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> deleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Category> GetAll()
        {
            return _ctx.Category.AsQueryable();

        }

        public IQueryable<Category> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int update(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
