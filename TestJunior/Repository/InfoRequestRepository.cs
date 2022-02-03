using System.Linq;

namespace TestJunior.Repository
{

    public class InfoRequestRepository : IRepository<InfoRequest>
    {
        private readonly DatabaseContext _ctx;

        public InfoRequestRepository(DatabaseContext ctx)
        {
            _ctx = ctx;
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
    }
}
