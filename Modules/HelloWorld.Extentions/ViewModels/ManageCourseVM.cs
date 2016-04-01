using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite.Common.Models.ViewModels;

namespace WebSite.ViewModels
{
    public class ManageCourseVM
    {
        public ManageCourseVM()
        {
        }

        public ManageCourseVM(CourseVM course, CourseFlowVM flow, IEnumerable<SignupApplication> applications, IEnumerable<StudentVM> students)
        {
            this.Course = course;
            this.Flow = flow;

            this.Registrants = new List<SignupApplication>(applications);
            this.Students = new List<StudentVM>(students);
        }

        public IEnumerable<SignupApplication> Applications
        {
            get { return this.Registrants; }
        }

        public IEnumerable<StudentVM> RegisteredStudents
        {
            get { return this.Students; }
        } 

        public CourseVM  Course { get; protected set; }
        public CourseFlowVM Flow { get; protected set; }


        protected List<SignupApplication> Registrants { get; set; }
        protected List<StudentVM>         Students { get; set; }
    }
}