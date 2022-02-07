using System.Linq;
using System.Threading.Tasks;

namespace TestJunior.Repository
{

    public class InfoRequestRepository : IRepository<InfoRequest>
    {
        private readonly DatabaseContext _ctx;

        public InfoRequestRepository(DatabaseContext ctx)
        {
            _ctx = ctx;
        }

        public void add(InfoRequest entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> deleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<InfoRequest> GetAll()
        {
            var AllRequests = _ctx.InfoRequest.AsQueryable();
            return AllRequests;
        }

        public IQueryable<InfoRequest> GetById(int id)
        {
            var SingleInforequest= _ctx.InfoRequest.Where(x => x.Id == id);
            return SingleInforequest;
        }

        public void update(InfoRequest entity)
        {
            throw new System.NotImplementedException();
        }

        Task<int> IRepository<InfoRequest>.add(InfoRequest entity)
        {
            throw new System.NotImplementedException();
        }


        Task<int> IRepository<InfoRequest>.update(InfoRequest entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
