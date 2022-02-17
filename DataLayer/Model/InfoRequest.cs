using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DataLayer
{
    /// <summary>
    /// class that represent the InfoRequest table of the DataBase
    /// </summary>
    public class InfoRequest
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
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

        /// property that represents the foreign key between Product and InfoRequest
        public  Product Product { get; set; }
        /// property that represents the foreign key between Nation and InfoRequest
        public  Nation Nation { get; set; }
        /// property that represents the foreign key between User and InfoRequest
        public  User User { get; set; }
        /// property that represents the foreign key between InfoRequestReply and InfoRequest
        public  IEnumerable<InfoRequestReply> InfoRequestReplies { get; set; } = new List<InfoRequestReply>();

    }
}
