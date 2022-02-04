using System.Linq;
using TestJunior.DetailedEntities;

namespace TestJunior.Services
{
    public interface IRequestServices
    {
        public IQueryable<InfoRequest> OrderedRequests(bool asc_desc);
        public PaginatedList<PaginatedRequest> ListOfRequest(int pagenumber, int pagesize,bool asc_desc);
        public IQueryable<APIRequestDetail> RequestDetail(int id);
    }
}
