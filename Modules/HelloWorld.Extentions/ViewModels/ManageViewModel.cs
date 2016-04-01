using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite.Common.Models.ViewModels;

namespace WebSite.ViewModels
{
    public class ManageViewModel
    {
        private List<Tuple<CourseFlowVM, CourseVM>> _flows = new List<Tuple<CourseFlowVM, CourseVM>>();

        public void AddFlow(CourseFlowVM flow,CourseVM course)
        {
            _flows.Add(new Tuple<CourseFlowVM, CourseVM>(flow, course));
        }


        public IEnumerable<Tuple<CourseFlowVM, CourseVM>> Flows
        {
            get { return _flows.OrderBy(g => g.Item2.CourseGroupId); }
        }
    }
}