using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.DetailedEntities;
using DataLayer.Repository;
using BusinessUnit.Services.Interfaces;

namespace TestJunior.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ValidationController
    {

        private readonly IProductServices _productServices;
        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id of product selected</param>
        /// <returns>returns a product with his list of categories if the id is higher than 0</returns>
        [HttpGet("{id}")]
        public IActionResult GetProductWithCategories(int id)
        {
            if (id > 0)
                return Ok(_productServices.GetSingleProduct(id));
            else
                return BadRequest("Id less or equal 0");
        }


        /// <summary>
        /// adding a product
        /// </summary>
        /// <param name="productModel">product to be added</param>
        /// <returns>
        /// a success status code if the product has been added
        /// or a bad request if there was an error
        /// </returns>
        [HttpPost("new")]
        public IActionResult AddProduct(APIProductWithCategories productModel)
        {
            ProductValidation(productModel);
            if(ModelState.IsValid && _productServices.AddProduct(productModel) != 0)
                    return Ok(productModel.Product.ProductId);
            else
                return BadRequest(ModelState);
        }

        /// <summary>
        /// updating a product
        /// </summary>
        /// <param name="productModel">product to be updated</param>
        /// <returns>
        /// success if the product has been updated
        /// or bad request if there was an error
        /// </returns>
        [HttpPut("update")]
        public IActionResult UpdateProduct(APIProductWithCategories productModel)
        {
            if (_productServices.UpdateProduct(productModel) != 0)
                return Ok(productModel.Product.ProductId);
            else
                return BadRequest("Error during update");
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
        public IActionResult GetAllPaginatedProducts(int pagenumber = 1, int pagesize = 10, int order = 0, bool asc_desc = true, string brandName = "")
        {

            if (pagenumber <= 0)
                return BadRequest("pagenumber is 0 or negative");
            if (pagesize <= 0)
                return BadRequest("pagesize is 0 or negative");
            return Ok(_productServices.ListOfProducts(pagenumber, pagesize, order, asc_desc, brandName));

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
        /// a bad request if id is 0 or negative
        /// a success if the product has been deleted
        /// a not found if the product is not fount
        /// </returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            if (id <= 0)
                return BadRequest("Id can't be 0 or negative");
            if (await _productServices.DeleteProductAsync(id) == 1)
                return Ok("item deleted");
            else
                return NotFound("item not found");
        }

    }
}