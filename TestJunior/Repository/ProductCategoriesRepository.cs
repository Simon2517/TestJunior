using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestJunior.DetailedEntities;

namespace TestJunior.Repository
{

    public class ProductCategoriesRepository : IRepository<Product>
    {
        private readonly DatabaseContext _ctx;
        public ProductCategoriesRepository(DatabaseContext ctx)
        {
            _ctx = ctx;
        }

        public int add(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> deleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Product> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
