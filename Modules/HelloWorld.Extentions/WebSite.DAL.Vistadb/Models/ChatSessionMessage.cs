using System;
using System.Collections.Generic;

namespace WebSite.DAL.Db.Models
{
    public partial class ChatSessionMessage
    {
        public int MessageId { get; set; }
        public System.Guid SessionId { get; set; }
        public string Author { get; set; }
        public System.DateTime PostedOn { get; set; }
        public string MessageText { get; set; }
        public virtual ChatSession ChatSession { get; set; }
        public int? MessageType { get; set; }
        public ulong PostOrderIndex { get; set; }
    }
}
