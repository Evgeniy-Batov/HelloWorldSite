using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Orchard.Projections.Descriptors.Filter;
using IFilterProvider = Orchard.Projections.Services.IFilterProvider;
using Orchard.Localization;
using HelloWorld.Extentions.Models;

namespace HelloWorld.Extentions.Filters
{
    public class CoursePartFilter : IFilterProvider
    {
        public Localizer T { get; set; }

        public CoursePartFilter()
        {
            T = NullLocalizer.Instance;
        }

        public void Describe(DescribeFilterContext describe)
        {
            describe.For(
             "Content",          // The category of this filter
             T("Content"),       // The name of the filter (not used in 1.4)
             T("Content"))       // The description of the filter (not used in 1.4)

             // Defines the actual filter (we could define multiple filters using the fluent syntax)
             .Element(
                 "CourseParts",     // Type of the element
                 T("Course  Parts"), // Name of the element
                 T("Course parts"), // Description of the element
                 ApplyFilter,        // Delegate to a method that performs the actual filtering for this element
                 DisplayFilter       // Delegate to a method that returns a descriptive string for this element
             );
        }

        private void ApplyFilter(FilterContext context)
        {

            // Set the Query property of the context parameter to any IHqlQuery. In our case, we use a default query
            // and narrow it down by joining with the ProductPartRecord.
            context.Query = context.Query.Join(x => x.ContentPartRecord(typeof(CoursePartRecord)));
        }

        private LocalizedString DisplayFilter(FilterContext context)
        {
            return T("Content with CoursePart");
        }
    }
}