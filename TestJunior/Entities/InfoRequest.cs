using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TestJunior
{
    public class InfoRequest
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }
        public string RequestText { get; set; }
        public DateTime InsertedDate { get; set; }

        public virtual Product Product { get; set; }
        public virtual Nation Nation { get; set; }
        public virtual User User { get; set; }
        public virtual IEnumerable<InfoRequestReply> InfoRequestReplies { get; set; } = new List<InfoRequestReply>();

    }
}
