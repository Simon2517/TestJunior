using System.Collections.Generic;
using TestJunior.DetailedEntities;

namespace TestJunior.Services
{
    public interface ICategoryServices
    {
        public List<APICategory> GetAllCategories();
    }
}