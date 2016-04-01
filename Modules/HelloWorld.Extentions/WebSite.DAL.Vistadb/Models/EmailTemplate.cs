using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  WebSite.DAL.Db.Models
{
    public class EmailTemplateDbModel
    {
        public int TemplateID { get; set; }
        public String HtmlVersion { get; set; }
        public String TextVersion { get; set; }
    }
}
