using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.Common.Models.ViewModels
{
    public class FeedbackVM
    {
        public int Id { get; set; }
        public String StudentName { get; set; }
        public String CourseName { get; set; }
        public String CourseRef { get; set; }
        public int CourseId { get; set; }
        public String FeedBack { get; set; }
        public String ImageRef { get; set; }
        public String VKProfileRef { get; set; }
        public String FBProfileRef { get; set; }
        public String OKProfileRef { get; set; }
        public DateTime PostTime { get; set; }
    }
}
