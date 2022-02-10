using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TestJunior.DetailedEntities;
using TestJunior.Repository;
using TestJunior.Services;

namespace TestJunior.Controllers
{
        [ApiController]
        [Route("[controller]")] 
    public class InfoRequestController : ControllerBase
    {

        private readonly IRequestServices _inforequestServices;
        public InfoRequestController(IRequestServices inforequestServices)
        {
            _inforequestServices = inforequestServices;
        }

        /// <summary>
        /// method for the pagination of brands
        /// </summary>
        /// <param name="pagenumber">Number of the page requested</param>
        /// <param name="pagesize">size of every page</param>
        /// <returns>
        /// A Bad request if either pagenumber or pagesize are 0 or below
        /// A paginated list of Brands if the input parameters are valid</returns>
        [HttpGet("requests/{pagenumber}/{pagesize}/{asc_desc}/{brandId}/{search?}")]
        public IActionResult GetAllPaginatedRequests(int pagenumber=1,int pagesize=10,bool asc_desc=false,int brandId=0,string search="")
        {

            if (pagenumber <= 0)
                return BadRequest("pagenumber is 0 or negative");
            if(pagesize <= 0)
                return BadRequest("pagesize is 0 or negative");
            return Ok(_inforequestServices.ListOfRequest(pagenumber, pagesize, asc_desc, brandId, search));

        }


        /// <summary>
        /// method to get the details of the Brand requested
        /// </summary>
        /// <param name="id">number of the brand id</param>
        /// <returns>
        /// A bad request if the id is 0 or negative
        /// A brand with its details if the id is valid
        /// </returns>
        [HttpGet("detail/{id}")]
        public IActionResult GetRequestDetail(int id)
        {
            if (id <= 0)
                return BadRequest("Id can't be 0 or negative");

            return Ok(_inforequestServices.RequestDetail(id).FirstOrDefault());
        }

    }
}
