using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestJunior.DetailedEntities;
using TestJunior.Repository;
using TestJunior.Services;

namespace TestJunior.Controllers
{
        [ApiController]
        [Route("[controller]")] 
    public class ProductController : ControllerBase
    {

        private readonly IProductServices _productServices;
        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        /// <summary>
        /// method for the pagination of brands
        /// </summary>
        /// <param name="pagenumber">Number of the page requested</param>
        /// <param name="pagesize">size of every page</param>
        /// <returns>
        /// A Bad request if either pagenumber or pagesize are 0 or below
        /// A paginated list of Brands if the input parameters are valid</returns>
        [HttpGet("products/{pagenumber}/{pagesize}/{order}/{asc_desc}/{brandname?}")]
        public IActionResult GetAllPaginatedProducts(int pagenumber=1, int pagesize=10,int order=0,bool asc_desc=true,string brandName="")
        {

            if (pagenumber <= 0)
                return BadRequest("pagenumber is 0 or negative");
            if(pagesize <= 0)
                return BadRequest("pagesize is 0 or negative");
            return Ok(_productServices.ListOfProducts(pagenumber, pagesize,order,asc_desc,brandName));

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
        public IActionResult GetProductDetail(int id)
        {
            if (id <= 0)
                return BadRequest("Id can't be 0 or negative");

            return Ok(_productServices.ProductDetail(id).FirstOrDefault());
        }

        /// <summary>
        /// soft delete of a product and all its relationed entities records
        /// </summary>
        /// <param name="id">id of the product to delete</param>
        /// <returns>
        /// 
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            if (id <= 0)
                return BadRequest("Id can't be 0 or negative");
            if(await _productServices.DeleteProductAsync(id)==1)
                return Ok("item deleted successfully");
            else
                return NotFound("item not found");
        }

        [HttpGet("{id}")]
        public IActionResult GetProductWithCategories(int id)
        {
            return Ok(_productServices.GetSingleProduct(id));
        }

        [HttpPost("new")]
        public IActionResult AddProduct(APIProductWithCategories product)
        {
            if (_productServices.AddProduct(product) != 0)
                return Ok();
            else
                return BadRequest("Errore nell'aggiunta");
        }

        [HttpPut("update")]
        public IActionResult UpdateProduct(APIProductWithCategories product)
        {
            if (_productServices.UpdateProduct(product) != 0)
                return Ok();
            else
                return BadRequest("Errore nell'aggiunta");
        }

    }
}
