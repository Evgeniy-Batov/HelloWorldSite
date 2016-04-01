using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.DAL.Db.Models
{
    public class EmailDbModel
    {
        public int EmailId { get; set; }
        public String From { get; set; }
        public String Subject { get; set; }
        public String Body { get; set; }
        public DateTime? SentTime { get; set; }
        public int Status { get; set; }
        public bool IsHtml { get; set; }
        public ICollection<EmailRecipientDbModel> Recipients { get; set; }
    }
}
