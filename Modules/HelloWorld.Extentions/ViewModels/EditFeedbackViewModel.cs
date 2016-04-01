using HelloWorld.Extentions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloWorld.Extentions.ViewModels
{
    public class EditFeedbackViewModel
    {
        public string Author { get; set; }
        public int CourseId { get; set; }
        public IEnumerable<CoursePartRecord> Courses { get; set; }
    }
}