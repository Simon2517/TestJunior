using System.Linq;
using System.Threading.Tasks;

namespace TestJunior.Repository
{
    public interface IRepository<T>
    {
        public IQueryable<T> GetById(int id);
        public IQueryable<T> GetAll();

        public Task<int> deleteAsync(int id);
        public Task<int> update(T entity);
        public Task<int> add(T entity);
    }
}
