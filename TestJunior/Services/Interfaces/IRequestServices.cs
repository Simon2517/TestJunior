using System.Linq;
using System.Threading.Tasks;
using TestJunior.DetailedEntities;

namespace TestJunior.Services
{
    public interface IRequestServices
    {
        public IQueryable<InfoRequest> OrderedRequests(int brandId, int productId, string search,bool asc_desc);
        public IQueryable<InfoRequest> SearchFilter(int brandId, int productId, string search);
        public PaginatedList<PaginatedRequest> ListOfRequest(int pagenumber, int pagesize,bool asc_desc,int brandId, int productId, string search);
        public IQueryable<APIRequestDetail> RequestDetail(int id);
        

    }
}
