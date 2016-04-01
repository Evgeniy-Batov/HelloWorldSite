using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.DAL.Db.Models.Models
{
    public class CoursePriceDbM
    {
        public int PriceId { get; set; }

        public virtual CourseDbM Course { get; set; }
        public int CourseId { get; set; }
        public decimal Price { get; set; }
        public String Conditional { get; set; }
        public String ShortConditional { get; set; }
        public String CustomCSS { get; set; }
        public DateTime? ValidTill { get; set; }

        public int NumberOfMonths { get; set; }
    }
}
