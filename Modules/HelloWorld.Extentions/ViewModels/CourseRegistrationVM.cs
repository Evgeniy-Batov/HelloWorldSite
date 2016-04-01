using HelloWorld.Extentions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloWorld.Extentions.ViewModels
{
    public class CourseRegistrationVM
    {
        public StudentRegistrationPart BlockData { get; set; }
		public IEnumerable<CoursePartRecord> Courses { get; set; }

    }
}