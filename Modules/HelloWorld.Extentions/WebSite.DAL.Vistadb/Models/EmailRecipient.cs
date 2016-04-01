using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.DAL.Db.Models
{
    public class EmailRecipientDbModel
    {
        public int    RecepientId { get; set; }
        public int    EmailId { get; set; }
        public String Recepient { get; set; }
        public bool   To { get; set; }
        public bool   CC { get; set; }
        public bool   BCC { get; set; }
        public bool   Phone { get; set; }

        public virtual EmailDbModel Email { get; set; }
    }
}
