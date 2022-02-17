using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using DataLayer.DetailedEntities;
using DataLayer.Repository;
using BusinessUnit.Services.Interfaces;

namespace TestJunior.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
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
