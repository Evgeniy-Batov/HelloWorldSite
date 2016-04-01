using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.DAL.Db.Models
{
    public partial class OfflineMessage
    {
        public OfflineMessage()
        {
        }

        public Int32 MessageId { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public String Topic { get; set; }
        public String Message { get; set; }
        public DateTime CreatedOn { get; set; }
        public String IP { get; set; }
    }
}
