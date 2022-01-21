

using Newtonsoft.Json;
using System.Collections.Generic;

namespace TestJunior
{
    public class Account
    {
        public int Id { get; set; }
        public string Email { get;  set; }
        public string Password { get;  set; }
        public byte AccountType { get;  set; }
        public virtual Brand Brand { get;  set; }
        public virtual User User { get;  set; }
        public virtual IEnumerable<InfoRequestReply> InfoRequestReplies { get; set; } = new List<InfoRequestReply>();
    }
}
