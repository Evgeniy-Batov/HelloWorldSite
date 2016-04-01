using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebSite.Models
{
    [DataContract]
    public class MeetingSessionVM
    {
        [Required]
        [DataMember(IsRequired=true)]
        public Guid SessionId     {get;set;}
        [Required]
        [DataMember(IsRequired = true)]
        public uint LastMessageId { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsBotProcessing { get; set; }
    }

    [DataContract]
    public class SendMessageVM:MeetingSessionVM
    {
        [Required]
        [DataMember]
        public String Message { get; set; }

        [Required]
        [DataMember]
        public String Author { get; set; }
    }

    [DataContract]
    public class MeetingMessageVM
    {
        [DataMember]
        public UInt64 MessageId { get; set; }
        [DataMember]
        public String   Author { get; set; }
        [DataMember]
        public DateTime PostedOn { get; set; }
        [DataMember]
        public String MessageText { get; set; }
        [DataMember]
        public int? MessageType { get; set; }

        public bool ClientMessage
        {
            get {return this.Author == "Пользователь";}
        }
    }
}