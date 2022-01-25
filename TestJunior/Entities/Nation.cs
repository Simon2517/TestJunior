using Newtonsoft.Json;
using System.Collections.Generic;

namespace TestJunior
{
    /// <summary>
    /// class that represent the Nation table of the DataBase
    /// </summary>
    public class Nation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        /// property that represents the foreign key betwen Nation and Inforequest
        public virtual IEnumerable<InfoRequest> InfoRequests { get; set; }=new List<InfoRequest>();
    }
}
