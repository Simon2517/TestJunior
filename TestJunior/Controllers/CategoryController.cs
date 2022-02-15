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
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryServices _CategoryServices;
        public CategoryController(ICategoryServices CategoryServices)
        {
            _CategoryServices = CategoryServices;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>full list of Categories</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_CategoryServices.GetAllCategories());
        }

    }
}
