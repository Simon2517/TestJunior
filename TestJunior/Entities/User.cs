using Newtonsoft.Json;
using System.Collections.Generic;

namespace TestJunior
{
    public class User
    {
        /// <summary>
        /// class that represent the User table of the DataBase
        /// </summary>
        public int Id { get; set; }
        public int? AccountId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        /// property that represents the foreign key between User and Account
        public virtual Account Account { get; set; }
        /// property that represents the foreign key between User and InfoRequest
        public IEnumerable<InfoRequest>InfoRequests { get; set; }=new List<InfoRequest>();
    }
}
