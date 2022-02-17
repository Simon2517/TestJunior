using System.Collections.Generic;
using System.Linq;
using BusinessUnit.Services.Interfaces;
using DataLayer;
using DataLayer.DetailedEntities;
using DataLayer.Repository;

namespace BusinessUnit.Services
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
            List<APICategory> list = _Categoryrepo.GetAll().Select(x => new APICategory
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
            return list;
        }
    }
}
