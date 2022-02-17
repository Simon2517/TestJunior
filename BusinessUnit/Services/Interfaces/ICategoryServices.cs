using System.Collections.Generic;
using DataLayer.DetailedEntities;

namespace BusinessUnit.Services.Interfaces
{
    public interface ICategoryServices
    {
        public List<APICategory> GetAllCategories();
    }
}