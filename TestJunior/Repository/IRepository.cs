using System.Linq;

namespace TestJunior.Repository
{
    public interface IRepository<T>
    {
        public IQueryable<T> GetById(int id);
        public IQueryable<T> GetAll();
    }
}
