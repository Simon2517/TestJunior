using System.Linq;
using TestJunior.DetailedEntities;
using TestJunior.Repository;

namespace TestJunior.Services
{
    public class RequestServices:IRequestServices
    {
        private readonly IRepository<InfoRequest> _Requestrepo;
        public RequestServices(IRepository<InfoRequest> _requestrepo)
        {
            _Requestrepo = _requestrepo;
        }
        public PaginatedList<PaginatedRequest> ListOfRequest(int pagenumber, int pagesize,bool asc_desc)
        {
            if (pagenumber <= 0 || pagesize <= 0)
                return null;
                
                IQueryable<PaginatedRequest> Reqs = OrderedRequests(asc_desc)
                    .Select(reqs => new PaginatedRequest
                    {
                        Id=reqs.Id,
                        Name = reqs.Name+' '+reqs.LastName,
                        BrandName = reqs.Product.Brand.BrandName,
                        ProductName = reqs.Product.Name,
                        RequestedDate=reqs.InsertedDate
                    });
            return PaginatedList<PaginatedRequest>.Create(Reqs, pagenumber, pagesize);
        }

        public IQueryable<InfoRequest> OrderedRequests(bool asc_desc)
        {
            var reqs = _Requestrepo.GetAll();
            if (asc_desc)
                return reqs.OrderBy(x => x.InsertedDate);
            else
                return reqs.OrderByDescending(x => x.InsertedDate);
            

        }

        public IQueryable<APIRequestDetail> RequestDetail(int id)
        {
            ///validation for Id <=0 or Id not found in the table
            var InforequestDetail = _Requestrepo.GetAll()
                .Select(info => new APIRequestDetail
                {
                    Id = info.Id,
                    Name = info.Name + " " + info.LastName,
                    Email = info.Email,
                    Address = info.City + "(" + info.PostalCode + ")," + info.Nation.Name,
                    Product = new PolishedProduct
                    {
                        ProductId = info.ProductId,
                        ProductName = info.Product.Name,
                        Brandname = info.Product.Brand.BrandName,
                    },
                    Replies = info.InfoRequestReplies.OrderByDescending(info => info.InsertedDate)
                    .Select(rep => new APIReply
                    {
                        Id = rep.Id,
                        Name = rep.Account.AccountType == 1 ? info.Product.Brand.BrandName : info.Name + " " + info.LastName,
                        ReplyText = rep.ReplyText
                    }),

                })
                .Where(info => info.Id == id);
            return InforequestDetail;
        }
    }
}
