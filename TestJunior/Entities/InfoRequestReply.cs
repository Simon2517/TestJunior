using Newtonsoft.Json;
using System;

namespace TestJunior
{
    public class InfoRequestReply
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int InfoRequestId { get; set; }
        public string ReplyText { get; set; }
        public DateTime InsertedDate { get; set; }
        public virtual InfoRequest InfoRequest { get; set; }
        public virtual Account Account { get; set; }
    }
}
