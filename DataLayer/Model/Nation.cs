using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataLayer
{
    /// <summary>
    /// class that represent the Nation table of the DataBase
    /// </summary>
    public class Nation
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public string Name { get; set; }
        /// property that represents the foreign key between Nation and Inforequest
        public  ICollection<InfoRequest> InfoRequests { get; set; }=new List<InfoRequest>();
    }
}
