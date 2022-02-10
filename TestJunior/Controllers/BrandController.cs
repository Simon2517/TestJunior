using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TestJunior.DetailedEntities;
using TestJunior.Repository;
using TestJunior.Services;

namespace TestJunior.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrandController : ControllerBase
    {

        private readonly IBrandServices _brandServices;
        public BrandController(IBrandServices brandServices)
        {
            _brandServices = brandServices;
        }

        /// <summary>
        /// method for the pagination of brands
        /// </summary>
        /// <param name="pagenumber">Number of the page requested</param>
        /// <param name="pagesize">size of every page</param>
        /// <returns>
        /// A Bad request if either pagenumber or pagesize are 0 or below
        /// A paginated list of Brands if the input parameters are valid</returns>
        [HttpGet("brands/{pagenumber}/{pagesize}/{order}/{asc_desc}")]
        public IActionResult GetAllPAginatedBrands(int pagenumber = 1, int pagesize = 10, int order = 0, bool asc_desc = true)
        {

            if (pagenumber <= 0)
                return BadRequest("pagenumber is 0 or negative");
            if (pagesize <= 0)
                return BadRequest("pagesize is 0 or negative");
            return Ok(_brandServices.ListOfBrands(pagenumber, pagesize, order, asc_desc));

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
        public IActionResult GetBrandDetail(int id)
        {
            if (id <= 0)
                return BadRequest("Id can't be 0 or negative");

            return Ok(_brandServices.BrandDetail(id).FirstOrDefault());
        }

        /// <summary>
        /// list of brand names used to filter the products
        /// </summary>
        /// <returns>list of strings</returns>
        [HttpGet("brandnames")]
        public IActionResult GetAllBrandNames()
        {
            return Ok(_brandServices.GetAllBrandNames());
        }


        [HttpGet("edit/{id}")]
        public IActionResult EditBrand(int id)
        {
            return Ok();
        }


        [HttpPost("new")]
        public IActionResult AddBrand(BrandViewModel brand)
        {
            if (_brandServices.AddBrand(brand) != 0)
                return Ok();
            else
                return BadRequest("Errore nell'aggiunta");
        }

    }
}
