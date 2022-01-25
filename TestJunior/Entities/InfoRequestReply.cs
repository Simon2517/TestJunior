using Newtonsoft.Json;
using System;

namespace TestJunior
{
    /// <summary>
    /// class that represent the InfoRequestReply table of the DataBase
    /// </summary>
    public class InfoRequestReply
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int InfoRequestId { get; set; }
        public string ReplyText { get; set; }
        public DateTime InsertedDate { get; set; }
        /// property that represents the foreign key betwen InfoRequest and InfoRequestReply
        public virtual InfoRequest InfoRequest { get; set; }
        /// property that represents the foreign key betwen Account and InfoRequestReply
        public virtual Account Account { get; set; }
    }
}
