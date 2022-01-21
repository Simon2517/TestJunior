using Newtonsoft.Json;
using System.Collections.Generic;

namespace TestJunior
{
    public class User
    {
        public int Id { get; set; }
        public int? AccountId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public virtual Account Account { get; set; }
        public IEnumerable<InfoRequest>InfoRequests { get; set; }=new List<InfoRequest>();
    }
}
