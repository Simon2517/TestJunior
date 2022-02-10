using System.Linq;
using TestJunior.DetailedEntities;

namespace TestJunior.Services
{
    public interface IRequestServices
    {
        public IQueryable<InfoRequest> OrderedRequests(int brandId, string search,bool asc_desc);
        public IQueryable<InfoRequest> SearchFilter(int brandId,string search);
        public PaginatedList<PaginatedRequest> ListOfRequest(int pagenumber, int pagesize,bool asc_desc,int brandId,string search);
        public IQueryable<APIRequestDetail> RequestDetail(int id);
    }
}
