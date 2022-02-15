using System.Collections.Generic;
using System.Linq;
using TestJunior.DetailedEntities;
using TestJunior.Repository;

namespace TestJunior.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IRepository<Category> _Categoryrepo;
   
        public CategoryServices(IRepository<Category> categoryrepo)
        {
            _Categoryrepo = categoryrepo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>full list of categories with their id and Name</returns>
        public List<APICategory> GetAllCategories()
        {
            List<APICategory> list=_Categoryrepo.GetAll().Select(x=>new APICategory
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
            return list;
        }
    }
}
