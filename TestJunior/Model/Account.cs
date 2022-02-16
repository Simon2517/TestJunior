
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TestJunior
{
    /// <summary>
    /// class that represent the Account table of the DataBase
    /// </summary>
    public class Account
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public string Email { get;  set; }
        public string Password { get;  set; }
        public byte AccountType { get;  set; }
        /// property that represents the foreign key between Brand and Account
        public  Brand Brand { get;  set; }
        /// property that represents the foreign key between User and Account
        public  User User { get;  set; }
        /// property that represents the foreign key between InfoRequestReply and Account
        public  IEnumerable<InfoRequestReply> InfoRequestReplies { get; set; } = new List<InfoRequestReply>();
    }
}
