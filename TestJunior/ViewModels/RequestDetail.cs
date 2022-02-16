using System.Collections.Generic;

namespace TestJunior.DetailedEntities
{
    public class RequestDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public PolishedProduct Product { get; set; }
        public IEnumerable<APIReply> Replies { get; set; }=new List<APIReply>();
    }
}
