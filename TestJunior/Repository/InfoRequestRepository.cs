using Microsoft.EntityFrameworkCore;
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

        public int add(InfoRequest entity)
        {
            _ctx.InfoRequest.Update(entity);
            return _ctx.SaveChanges();
        }

        public async Task<int> deleteAsync(int id)
        {
            var inforequest = _ctx.InfoRequest.FirstOrDefault(x => x.Id == id);
            if (inforequest != null)
            {
                inforequest.isDeleted = true;

                await _ctx.Database.ExecuteSqlRawAsync(@"UPDATE InfoRequestReply
                                                         SET isDeleted=1
                                                         WHERE  InfoRequestId=" + id);
            }

            return _ctx.SaveChanges();
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

        public int update(InfoRequest entity)
        {
            _ctx.InfoRequest.Update(entity);
            return _ctx.SaveChanges();
        }
    }
}
