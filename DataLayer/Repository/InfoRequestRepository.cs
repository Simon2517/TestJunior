using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Repository
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
            int result = 0;
            using IDbContextTransaction transaction = _ctx.Database.BeginTransaction();
            try
            {
                var inforequest = _ctx.Product.FirstOrDefault(x => x.ProductId == id);
                inforequest.isDeleted = true;

                await _ctx.InfoRequestReply
                        .Where(reply => reply.InfoRequestId == id)
                            .UpdateFromQueryAsync(x => new InfoRequestReply { isDeleted = true });

                _ctx.Update(inforequest);
                result = _ctx.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            return result;
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
