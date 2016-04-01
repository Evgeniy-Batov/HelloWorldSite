using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Orchard.Projections.Descriptors.Filter;
using IFilterProvider = Orchard.Projections.Services.IFilterProvider;
using Orchard.Localization;
using HelloWorld.Extentions.Models;
using Orchard.ContentManagement;

namespace HelloWorld.Extentions.Filters
{
    public class FeedbackPartFilter : IFilterProvider
    {
        private readonly IContentManager _contentManager;

        public Localizer T { get; set; }

        public FeedbackPartFilter(IContentManager contentManager)
        {
            T = NullLocalizer.Instance;

            _contentManager = contentManager;
        }

        public void Describe(DescribeFilterContext describe)
        {
            describe.For(
           "Content",          // The category of this filter
           T("Content"),       // The name of the filter (not used in 1.4)
           T("Content"))       // The description of the filter (not used in 1.4)

           // Defines the actual filter (we could define multiple filters using the fluent syntax)
           .Element(
               "FeedbackParts",     // Type of the element
               T("Feedback Parts"), // Name of the element
               T("Feedback parts"), // Description of the element
               ApplyFilter,        // Delegate to a method that performs the actual filtering for this element
               DisplayFilter       // Delegate to a method that returns a descriptive string for this element
           );
        }

        private void ApplyFilter(FilterContext context)
        {
            String courseId = HttpContext.Current.Request.Params["course"];

            // Set the Query property of the context parameter to any IHqlQuery. In our case, we use a default query
            // and narrow it down by joining with the ProductPartRecord.

            if (String.IsNullOrEmpty(courseId))
            {
                context.Query = context.Query.Join(x => x.ContentPartRecord(typeof(FeedbackPartRecord)));
            }
            else
            {
                var coursePages = _contentManager.List<ContentPart>(new string[] { "CoursePage" });

                int courseIdInt = 0;

                foreach(var coursePage in coursePages)
                {
                    String urlPath = ((dynamic)coursePage.ContentItem).AutoroutePart.Path;

                    if (urlPath.Contains('/'))
                    {
                        if (urlPath.Split('/')[1] == courseId)
                        {
                            courseIdInt = coursePage.Id;
                            break;
                        }
                    }    
                }

                context.Query = context.Query.Where(x => x.ContentPartRecord<FeedbackPartRecord>(), x => x.Eq("CourseId", courseIdInt )); //.Join(x2 => x2.ContentPartRecord(typeof(FeedbackPartRecord))); 
            }
        }

        private LocalizedString DisplayFilter(FilterContext context)
        {
            return T("Content with FeedbackPart");
        }
    }
}