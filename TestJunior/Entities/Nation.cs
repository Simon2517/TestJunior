using Newtonsoft.Json;
using System.Collections.Generic;

namespace TestJunior
{
    public class Nation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<InfoRequest> InfoRequests { get; set; }=new List<InfoRequest>();
    }
}
