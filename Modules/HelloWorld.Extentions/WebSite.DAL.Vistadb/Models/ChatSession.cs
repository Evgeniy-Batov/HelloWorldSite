using System;
using System.Collections.Generic;

namespace WebSite.DAL.Db.Models
{
    public partial class ChatSession
    {
        public ChatSession()
        {
            this.ChatSessionMessages = new List<ChatSessionMessage>();
        }

        public System.Guid SessionId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public virtual ICollection<ChatSessionMessage> ChatSessionMessages { get; set; }
        public String IP { get; set; }
        public bool IsBotProcessing { get; set; }
    }
}
