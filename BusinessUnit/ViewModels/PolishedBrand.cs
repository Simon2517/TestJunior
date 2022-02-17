using System.Collections.Generic;

namespace BusinessUnit.ViewModels
{
    public class PolishedBrand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<int> ProductIds { get; set; }
    }
}
