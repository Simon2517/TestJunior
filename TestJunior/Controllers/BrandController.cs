using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.DetailedEntities;
using DataLayer.Repository;
using DataLayer;
using BusinessUnit.Services.Interfaces;

namespace TestJunior.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrandController : ValidationController
    {

        private readonly IBrandServices _brandServices;
        public BrandController(IBrandServices brandServices)
        {
            _brandServices = brandServices;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id of the brand searched</param>
        /// <returns>the brand searched if there was no error
        /// a bad request if the id is 0 or negative</returns>
        [HttpGet("{id}")]
        public IActionResult GetBrandById(int id)
        {
            if (id > 0)
                return Ok(_brandServices.GetSingleBrand(id));
            else
                return BadRequest("error");
        }

        /// <summary>
        /// method for the pagination of brands
        /// </summary>
        /// <param name="pagenumber">Number of the page requested</param>
        /// <param name="pagesize">size of every page</param>
        /// <returns>
        /// A Bad request if either pagenumber or pagesize are 0 or below
        /// A paginated list of Brands if the input parameters are valid</returns>
        [HttpGet("brands/{pagenumber}/{pagesize}/{search?}")]
        public IActionResult GetAllPAginatedBrands(int pagenumber = 1, int pagesize = 10, string search = "")
        {

            if (pagenumber <= 0)
                return BadRequest("pagenumber is 0 or negative");
            if (pagesize <= 0)
                return BadRequest("pagesize is 0 or negative");
            return Ok(_brandServices.ListOfBrands(pagenumber, pagesize, search));

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



        /// <summary>
        /// updating a product
        /// </summary>
        /// <param name="brand">brand to be updated</param>
        /// <returns>
        /// success if the brand has been updated
        /// or bad request if there was an error
        /// </returns>
        [HttpPut("update")]
        public IActionResult EditBrand(Brand brand)
        {
            if (brand == null)
                return BadRequest("error");

            return Ok(_brandServices.UpdateBrand(brand));

        }


        /// <summary>
        /// adding a product
        /// </summary>
        /// <param name="brandModel">brand to be added</param>
        /// <returns>
        /// a success status code if the brand has been added
        /// or a bad request if there was an error
        /// </returns>
        [HttpPost("new")]
        public IActionResult AddBrand(BrandViewModel brandModel)
        {

            BrandValidation(brandModel);
            if (!_brandServices.isEmailValid(brandModel.brand.Account.Email))
                ModelState.AddModelError("brand.account.email", "Email already exists");
            if (ModelState.IsValid)
                return Ok(_brandServices.AddBrand(brandModel));
            else
                return BadRequest(ModelState);
        }


        /// <summary>
        /// soft delete of a brand and all its relationed entities records
        /// </summary>
        /// <param name="id">id of the brand to delete</param>
        /// <returns>
        /// a bad request if id is 0 or negative
        /// a success if the brand has been deleted
        /// a not found if the brand is not fount
        /// </returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBrandAsync(int id)
        {
            if (id <= 0)
                return BadRequest("Id can't be 0 or negative");
            if (await _brandServices.DeleteBrandAsync(id) == 1)
                return Ok("item deleted successfully");
            else
                return NotFound("item not found");
        }

    }
}
